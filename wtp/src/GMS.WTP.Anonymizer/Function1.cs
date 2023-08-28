using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;

namespace GMS.WTP.Anonymizer
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([BlobTrigger("%WTP_PRODUCTION_FILE_PATH%", Connection = "WTP_PRODUCTION_STORAGE_ACCOUNT_CONNECTION_STRING")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
