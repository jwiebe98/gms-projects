using System;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class FormatDecimalForDisplay
    {
        public static string FormatForDisplay(this decimal? number, string type) => number is null ? null : Format((decimal)number, type);

        public static string FormatForDisplay(this decimal number, string type) => Format(number, type);

        private static string Format(decimal number, string type)
        {
            switch (type)
            {
                case DOLLAR:
                    return string.Format("{0:C}", number);

                case COUNT:
                    return number.ToString();

                default:
                    throw new FormatException($"Type: {type} unknown! Cannot format.");
            }
        }
    }
}
