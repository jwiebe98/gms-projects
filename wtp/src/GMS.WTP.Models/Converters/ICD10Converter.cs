using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace GMS.WTP.Models.Converters
{
    public class ICD10Converter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var parts = text.Split(" ");
                return parts[0];
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
    }

}
