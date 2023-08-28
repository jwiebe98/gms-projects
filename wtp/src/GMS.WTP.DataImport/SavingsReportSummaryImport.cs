using GMS.WTP.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace GMS.WTP.DataImport
{
    public static class SavingsReportSummaryImport
    {
        [FunctionName("ImportSavingsReportSummaryEventsToCIMS")]
        [ExponentialBackoffRetry(-1, "00:00:04", "00:15:00")]
        public static void Run([ServiceBusTrigger("savings-report-summary", "cims", Connection = "WTP_SERVICE_BUS_CONNECTION_STRING")] SavingsReportSummary savingsReportSummary, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function: ImportSavingsReportSummaryEventsToCIMS");

            log.LogInformation($"Inserting savings report summary event into CIMS table");
            savingsReportSummary.InsertIntoSavingsReportSummaryTable(log);
        }
    }
}
