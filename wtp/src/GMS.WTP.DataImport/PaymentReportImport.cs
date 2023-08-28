using GMS.WTP.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace GMS.WTP.DataImport
{
    public static class PaymentReportImport
    {
        [FunctionName("ImportPaymentReportEventsToCIMS")]
        [ExponentialBackoffRetry(-1, "00:00:04", "00:15:00")]
        public static void Run([ServiceBusTrigger("payment-report", "cims", Connection = "WTP_SERVICE_BUS_CONNECTION_STRING")] PaymentReport paymentReport, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function: ImportPaymentReportEventsToCIMS");

            log.LogInformation($"Inserting payment report event into CIMS table");
            paymentReport.InsertIntoPaymentReportTable(log);
        }
    }
}
