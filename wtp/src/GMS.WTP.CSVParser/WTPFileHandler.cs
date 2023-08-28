using GMS.WTP.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using static GMS.WTP.CSVParser.FileParser;

namespace GMS.WTP.CSVParser
{
    public static class WTPFileHandler
    {
        [FunctionName("ParseWTPFiles")]
        public static async Task Run([BlobTrigger("%WTP_BLOB_CONTAINER_PATH%", Connection = "WTP_STORAGE_ACCOUNT_CONNECTION_STRING")] Stream fileStream, string fileName, ILogger log)
        {
            if (fileName.Contains("GMS Bordereau Report"))
            {
                await ParseFile<ClaimBordereau>(log, fileName, fileStream, "claim-bordereau");
            }
            else if (fileName.Contains("Savings Report Detail"))
            {
                await ParseFile<SavingsReportDetail>(log, fileName, fileStream, "savings-report-detail");
            }
            else if (fileName.Contains("Savings Report Summary"))
            {
                await ParseFile<SavingsReportSummary>(log, fileName, fileStream, "savings-report-summary");
            }
            else if (fileName.Contains("Payment Report"))
            {
                await ParseFile<PaymentReport>(log, fileName, fileStream, "payment-report");
            }
            else
            {
                throw new Exception($"File {fileName} has no parser found!");
            }
        }
    }
}
