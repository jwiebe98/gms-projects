using FlatFiles.TypeMapping;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.PredeterminationFileMapperTypeSelector;


namespace GMS.ESC.FileParser
{
    public class ParsePredeterminationFile
    {
        [FunctionName("PredeterminationFileParser")]
        public void Run([BlobTrigger("esc-files/claims/predetermination/{fileName}", Connection = "ESC_STORAGE_ACCOUNT_CONNECTION_STRING")] Stream myBlob, string fileName, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{fileName} \n Size: {myBlob.Length} Bytes");

            StreamReader fileReader = new StreamReader(myBlob);
            string file = fileReader.ReadToEnd();

            var reader = GetPredeterminationFileMapperTypeSelector().GetReader(new StringReader(file), new()
            {
                Alignment = FlatFiles.FixedAlignment.LeftAligned,
                FillCharacter = ' '
            });

            var data = reader.ReadAll().ToList();

            log.LogInformation("asdf");
        }
    }
}
