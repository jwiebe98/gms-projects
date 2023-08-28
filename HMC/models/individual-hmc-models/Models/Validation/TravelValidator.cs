using System.ComponentModel.DataAnnotations;

namespace Gmsca.HelpMeChoose.Individual.Models.Validation
{
    public sealed class TravelValidator : ValidationAttribute
    {
        private const string ERROR_MESSAGE_NO_TRAVEL_DURATION = "Coverage type of 'TRAVEL' Must have values set for 'TravelDuration'.";

        private const string ERROR_MESSAGE_NO_COVERAGE_TYPE = "CoverageType must contain 'TRAVEL' if setting values for 'TravelDuration'.";

        private const string TRAVEL = "TRAVEL";

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

            if (questions.TravelDuration == "" && questions.CoverageType.Contains(TRAVEL))
            {
                return new ValidationResult(ERROR_MESSAGE_NO_TRAVEL_DURATION);
            }

            if (questions.TravelDuration != "" && !questions.CoverageType.Contains(TRAVEL))
            {
                return new ValidationResult(ERROR_MESSAGE_NO_COVERAGE_TYPE);
            }

            return ValidationResult.Success;
        }
    }
}
