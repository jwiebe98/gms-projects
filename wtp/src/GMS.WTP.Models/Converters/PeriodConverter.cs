using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace GMS.WTP.Models.Converters
{
    public class PeriodConverter : DateTimeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (DateTime.TryParseExact(text, "yyyyMM", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    return date.ToString("yyyy-MM-dd");
                }
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
