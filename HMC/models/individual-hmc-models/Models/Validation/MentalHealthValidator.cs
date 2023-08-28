using System.ComponentModel.DataAnnotations;

namespace Gmsca.HelpMeChoose.Individual.Models.Validation
{
    public sealed class MentalHealthValidator : ValidationAttribute
    {
        private const string MENTAL_HEALTH_SUPPORT = "MENTAL_HEALTH_SUPPORT";

        private const string ERROR_MESSAGE_NO_FREQUENCY = "Coverage type of 'MENTAL_HEALTH_SUPPORT' Must have values set for 'FrequencyOfMentalHealthVisits'.";

        private const string ERROR_MESSAGE_NO_COVERAGE_TYPE = "CoverageType must contain 'MENTAL_HEALTH_SUPPORT' if setting values for 'FrequencyOfMentalHealthVisits'.";

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

            if (questions.FrequencyOfMentalHealthVisits == "" && questions.CoverageType.Contains(MENTAL_HEALTH_SUPPORT))
            {
                return new ValidationResult(ERROR_MESSAGE_NO_FREQUENCY);
            }

            if (questions.FrequencyOfMentalHealthVisits != "" && !questions.CoverageType.Contains(MENTAL_HEALTH_SUPPORT))
            {
                return new ValidationResult(ERROR_MESSAGE_NO_COVERAGE_TYPE);
            }

            return ValidationResult.Success;
        }
    }
}
