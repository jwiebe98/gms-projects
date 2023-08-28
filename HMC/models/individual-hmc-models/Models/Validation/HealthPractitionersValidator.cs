using System.ComponentModel.DataAnnotations;

namespace Gmsca.HelpMeChoose.Individual.Models.Validation
{
    public sealed class HealthPractitionersValidator : ValidationAttribute
    {
        private const string HEALTH_PRACTITIONERS = "HEALTH_PRACTITIONERS";

        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is not Questions)
            {
                return new ValidationResult("HealthPractitionersValidator must be used on Questions");
            }

            Questions questions = (Questions)value;

            if (questions.HealthCarePractitionerType.Count == 0 && questions.CoverageType.Contains(HEALTH_PRACTITIONERS))
            {
                return new ValidationResult($"Coverage type of '{HEALTH_PRACTITIONERS}' Must have values set for 'HealthCarePractitionerType'.");
            }

            if (questions.HealthCarePractitionerType.Count > 0 && !questions.CoverageType.Contains(HEALTH_PRACTITIONERS))
            {
                return new ValidationResult($"CoverageType must contain '{HEALTH_PRACTITIONERS}' if setting values for 'HealthCarePractitionerType'.");
            }

            return ValidationResult.Success;
        }
    }
}
