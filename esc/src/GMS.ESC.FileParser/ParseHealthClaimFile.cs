using FlatFiles.TypeMapping;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.HealthClaimFileMapperTypeSelector;

namespace GMS.ESC.FileParser
{
    public class ParseHealthClaimFile
    {
        [FunctionName("ESCHealthClaimFileParser")]
        public void Run([BlobTrigger("esc-files/claims/ehc/{fileName}", Connection = "ESC_STORAGE_ACCOUNT_CONNECTION_STRING")] Stream myBlob, string fileName, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{fileName} \n Size: {myBlob.Length} Bytes");

            StreamReader fileReader = new StreamReader(myBlob);
            string file = fileReader.ReadToEnd();

            var reader = GetHealthClaimFileMapperTypeSelector().GetReader(new StringReader(file), new()
            {
                Alignment = FlatFiles.FixedAlignment.LeftAligned,
                FillCharacter = ' ',
            });

            var data = reader.ReadAll().ToList();

            string json = JsonConvert.SerializeObject(data[2]);

            /*
            
            each message to service bus should contain:

            File header and batch control
            relevant provider header and batch control
            related claim record
            each claim in the claim record is a separate event

             */

            log.LogInformation("asdf");
        }
    }
}
