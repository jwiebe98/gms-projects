using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace GMS.WTP.Models.Converters
{
    public class PercentageConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.EndsWith("%"))
                {
                    var percentageValue = text.TrimEnd('%');
                    if (decimal.TryParse(percentageValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var percentage))
                    {
                        return percentage / 100;
                    }
                }
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
    }

}
