using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    public sealed class StringRangeAttribute : ValidationAttribute
    {
        public string?[] AllowableValues { get; set; } = new string[0];

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (AllowableValues.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            string[] displayValues = AllowableValues.Select(el =>
            {
                el = el is null ? "null" : el;
                el = el is "" ? "\'\'" : el;
                return el;
            }).ToArray();

            string msg = $"Please enter one of the allowable values: {string.Join(", ", displayValues ?? new string[] { "No allowable values found" })}.";
            return new ValidationResult(msg);
        }
    }
}
