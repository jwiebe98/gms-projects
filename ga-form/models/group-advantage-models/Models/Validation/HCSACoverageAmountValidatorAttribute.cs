using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    public sealed class HCSACoverageAmountValidatorAttribute : ValidationAttribute
    {

        public override string FormatErrorMessage(string _)
        {
            return "Health Spending Coverage Amount must be a multiple of $50 and be between $250 and $15,000";
        }

        public override bool IsValid(object? value)
        {
            return value == null || (value is long && ((long)value is 0 || ((long)value >= 250 && (long)value <= 15000 && (long)value % 50 is 0)));
        }
    }
}
