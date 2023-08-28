using GMS.WTP.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace GMS.WTP.DataImport
{
    public static class ClaimBordereauImport
    {
        [FunctionName("ImportClaimBordereauEventsToCIMS")]
        [ExponentialBackoffRetry(-1, "00:00:04", "00:15:00")]
        public static void Run([ServiceBusTrigger("claim-bordereau", "cims", Connection = "WTP_SERVICE_BUS_CONNECTION_STRING")] ClaimBordereau claimBordereau, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function: ImportClaimBordereauEventsToCIMS");

            log.LogInformation($"Inserting claim bordereau event into CIMS table");
            claimBordereau.InsertIntoClaimBordereauTable(log);
        }
    }
}
