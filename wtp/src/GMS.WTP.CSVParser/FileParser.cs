using Azure.Messaging.ServiceBus;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static GMS.WTP.CSVParser.ServiceBusService;

namespace GMS.WTP.CSVParser
{
    public static class FileParser
    {
        public static async Task ParseFile<T>(ILogger log, string fileName, Stream fileStream, string topic)
        {
            using StreamReader fileData = new StreamReader(fileStream);

            using CsvReader csv = new CsvReader(fileData, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldSkipRecord = (args) => ShouldSkipHandler(log, args)
            });

            var messagesEnqueued = new Queue<ServiceBusMessage>();

            List<T> records = csv.GetRecords<T>().ToList();

            foreach (dynamic record in records)
            {
                try
                {
                    record.FileName = fileName;
                    record.RowNumber = records.IndexOf(record);

                    var message = new ServiceBusMessage(JsonConvert.SerializeObject(record));
                    message.MessageId = record.ESBMessageID;

                    message.ApplicationProperties.Add("FileName", fileName);
                    message.ApplicationProperties.Add("FileRow", records.IndexOf(record));
                    messagesEnqueued.Enqueue(message);
                }
                catch (Exception ex)
                {
                    await SendExceptionToServiceBus(log,
                        $"Can not send message exception error : '{ex.Message}'",
                        fileName);
                }
            }

            await SendBatchOfObjectsToServiceBusTopic(log, new()
            {
                FileName = fileName,
                FileRows = records.Count,
                MessagesEnqueued = messagesEnqueued,
                Topic = topic
            });
        }

        private static async Task SendExceptionToServiceBus(ILogger log, string exceptionMessage, string fileName)
        {
            log.LogError(exceptionMessage);
            var message = new ServiceBusMessage(exceptionMessage);
            message.ApplicationProperties.Add("FileName", fileName);

            await SendErrorObjectToServiceBusTopic(log, message, "wtp-failed-events");
        }

        private static bool ShouldSkipHandler(ILogger log, ShouldSkipRecordArgs args)
        {
            var rawRecord = args.Row.Parser.RawRecord.Replace("\r\n", "");
            var allCommas = rawRecord.All(c => c.Equals(','));

            if (allCommas)
            {
                log.LogWarning($"Skipping row '{args.Row.Parser.RawRow}' because there are no values: '{rawRecord}'");
                return true;
            }

            return false;
        }
    }
}
