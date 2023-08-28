using FlatFiles.TypeMapping;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using static GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers.DentalClaimFileMapperTypeSelector;

namespace GMS.ESC.FileParser
{
    public class ParseDentalClaimFile
    {
        [FunctionName("ESCDentalClaimFileParser")]
        public void Run([BlobTrigger("esc-files/claims/dental/{fileName}", Connection = "ESC_STORAGE_ACCOUNT_CONNECTION_STRING")] Stream myBlob, string fileName, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{fileName} \n Size: {myBlob.Length} Bytes");

            StreamReader fileReader = new StreamReader(myBlob);
            string file = fileReader.ReadToEnd();

            var reader = GetDentalClaimFileMapperTypeSelector().GetReader(new StringReader(file), new()
            {
                Alignment = FlatFiles.FixedAlignment.LeftAligned,
                FillCharacter = ' '
            });

            var data = reader.ReadAll().ToList();

            log.LogInformation("asdf");
        }
    }
}
