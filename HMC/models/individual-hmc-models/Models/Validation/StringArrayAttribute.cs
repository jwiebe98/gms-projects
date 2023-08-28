using System.ComponentModel.DataAnnotations;

namespace Gmsca.HelpMeChoose.Individual.Models.Validation
{
    public sealed class StringArrayAttribute : ValidationAttribute
    {
        private readonly string[] _options;

        public StringArrayAttribute(string[] options)
        {
            _options = options;
        }

        protected override ValidationResult? IsValid(object? values, ValidationContext validationContext)
        {
            if (values is null or not List<string>)
            {
                return new ValidationResult("Cannot be null or non-list item");
            }

            foreach (string value in (List<string>)values)
            {
                if (!_options.Contains(value))
                {
                    return new ValidationResult($"Invalid value \'{value}\', List can only contain: [{string.Join(", ", _options)}].");
                }
            }

            return ValidationResult.Success;
        }
    }
}
