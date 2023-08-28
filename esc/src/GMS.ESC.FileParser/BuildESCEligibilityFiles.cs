using FlatFiles.TypeMapping;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EligibilityFileMapperTypeSelector;

namespace GMS.ESC.FileParser
{
    public class BuildESCEligibilityFiles
    {
        [FunctionName("ESCEligibilityFileBuilder")]
        public void Run([BlobTrigger("esc-files/eligibility/{fileName}", Connection = "ESC_STORAGE_ACCOUNT_CONNECTION_STRING")] Stream myBlob, string fileName, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{fileName} \n Size: {myBlob.Length} Bytes");

            StreamReader fileReader = new StreamReader(myBlob);
            string file = fileReader.ReadToEnd();

            var reader = GetEligibilityFileMapperTypeSelector().GetReader(new StringReader(file), new()
            {
                Alignment = FlatFiles.FixedAlignment.LeftAligned,
                FillCharacter = ' '
            });

            var data = reader.ReadAll().ToList();

            log.LogInformation("asdf");
        }
    }
}
