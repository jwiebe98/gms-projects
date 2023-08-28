using System.ComponentModel.DataAnnotations;

namespace Gmsca.HelpMeChoose.Individual.Models.Validation
{
    public sealed class SpouseAgeValidator : ValidationAttribute
    {
        private const string SPOUSE = "SPOUSE";

        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is not Quote)
            {
                return new ValidationResult("SpouseAgeValidator must be used on Quote");
            }

            Quote quote = (Quote)value;

            if (quote.Questions.NumberPeopleCovered.Contains(SPOUSE) && quote.Applicant.SpouseAge == 0)
            {
                return new ValidationResult($"If Questions.NumberPeopleCovered contains 'SPOUSE' Applicant.SpouseAge must be set.");
            }

            if (!quote.Questions.NumberPeopleCovered.Contains(SPOUSE) && quote.Applicant.SpouseAge > 0)
            {
                return new ValidationResult($"If Applicant.SpouseAge is set Questions.NumberPeopleCovered must contain 'SPOUSE'.");
            }

            return ValidationResult.Success;
        }
    }
}
