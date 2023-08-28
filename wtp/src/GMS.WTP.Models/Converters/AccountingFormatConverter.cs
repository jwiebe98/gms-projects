using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace GMS.WTP.Models.Converters
{
    public class AccountingFormatConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return base.ConvertFromString(text, row, memberMapData);
            }

            var decimalValue = decimal.Parse(new string(text).Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());

            return text.Contains("(") && text.Contains(")") ? decimalValue * -1 : decimalValue;
        }
    }

}
