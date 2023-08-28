using GMS.WTP.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace GMS.WTP.DataImport
{
    public static class SavingsReportDetailImport
    {
        [FunctionName("ImportSavingsReportDetailEventsToCIMS")]
        [ExponentialBackoffRetry(-1, "00:00:04", "00:15:00")]
        public static void Run([ServiceBusTrigger("savings-report-detail", "cims", Connection = "WTP_SERVICE_BUS_CONNECTION_STRING")] SavingsReportDetail savingsReportDetail, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function: ImportSavingsReportDetailEventsToCIMS");

            log.LogInformation($"Inserting savings report detail event into CIMS table");
            savingsReportDetail.InsertIntoSavingsReportDetailTable(log);
        }
    }
}
