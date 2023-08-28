using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMS.WTP.CSVParser
{
    public static class ServiceBusService
    {
        public static async Task SendBatchOfObjectsToServiceBusTopic(ILogger log, ServiceBusEvent serviceBusEvent)
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            var serviceBusClient = new ServiceBusClient(EnvironmentVariables.WTP_SERVICE_BUS_CONNECTION_STRING, clientOptions);
            var sender = serviceBusClient.CreateSender(serviceBusEvent.Topic);

            var messageCount = serviceBusEvent.MessagesEnqueued.Count;

            while (serviceBusEvent.MessagesEnqueued.Count > 0)
            {
                using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

                if (!messageBatch.TryAddMessage(serviceBusEvent.MessagesEnqueued.Peek()))
                {
                    await sender.SendMessagesAsync((IEnumerable<ServiceBusMessage>)serviceBusEvent.MessagesEnqueued.Peek());
                    log.LogWarning($"Message {messageCount - serviceBusEvent.MessagesEnqueued.Count} ' : ' {serviceBusEvent.MessagesEnqueued.Peek()} is too large to batch and sent as a single message.");
                }

                do
                {
                    serviceBusEvent.MessagesEnqueued.Dequeue();
                }
                while (serviceBusEvent.MessagesEnqueued.Count > 0 && messageBatch.TryAddMessage(serviceBusEvent.MessagesEnqueued.Peek()));

                try
                {
                    await sender.SendMessagesAsync(messageBatch);
                }
                catch (Exception e)
                {
                    log.LogError($"Error sending batch of messages exception error : '{e.Message}'");
                }
                finally
                {
                    await sender.DisposeAsync();
                    await serviceBusClient.DisposeAsync();
                }
            }

            log.LogInformation($" File name : '{serviceBusEvent.FileName}'");
            log.LogInformation($" Total messages : '{serviceBusEvent.FileRows}'");
            log.LogInformation($" Passed messages : '{messageCount - serviceBusEvent.MessagesEnqueued.Count}'");
            log.LogError($" Failed messages : '{(serviceBusEvent.FileRows - messageCount) + serviceBusEvent.MessagesEnqueued.Count}'");

            await sender.DisposeAsync();
            await serviceBusClient.DisposeAsync();
            await sender.CloseAsync();
        }

        public static async Task SendErrorObjectToServiceBusTopic(ILogger log, ServiceBusMessage message, string topic)
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            var serviceBusClient = new ServiceBusClient(EnvironmentVariables.WTP_SERVICE_BUS_CONNECTION_STRING, clientOptions);

            var sender = serviceBusClient.CreateSender(topic);

            await sender.SendMessageAsync(message);

            log.LogError($"Sent '{message}' to '{topic}' topic");
            await sender.DisposeAsync();
            await serviceBusClient.DisposeAsync();
            await sender.CloseAsync();
        }
    }

    public class ServiceBusEvent
    {
        public string Topic { get; set; }
        public Queue<ServiceBusMessage> MessagesEnqueued { get; set; }
        public string FileName { get; set; }
        public int FileRows { get; set; }
    }
}
