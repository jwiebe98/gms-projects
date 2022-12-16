using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    public sealed class AgeValidatorAttribute : ValidationAttribute
    {
        private const int MIN = 18;
        private const int MAX = 65;

        public override string FormatErrorMessage(string _)
        {
            return $"Employee must be between the ages of {MIN} and {MAX}.";
        }

        public override bool IsValid(object? value)
        {
            return value is null || (value is int && (int)value is 0 or (>= MIN and <= MAX));
        }
    }
}

