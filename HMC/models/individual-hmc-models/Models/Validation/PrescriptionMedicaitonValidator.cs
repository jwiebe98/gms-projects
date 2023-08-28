using System.ComponentModel.DataAnnotations;

namespace Gmsca.HelpMeChoose.Individual.Models.Validation
{
    public sealed class PrescriptionMedicationValidator : ValidationAttribute
    {
        private const string ERROR_MESSAGE_NO_NUMBER_OF_DRUGS = "Coverage type of 'PRESCRIPTION_MEDICATION' Must have values set for 'NumberOfDrugPrescriptions'.";

        private const string ERROR_MESSAGE_NO_COVERAGE_TYPE = "CoverageType must contain 'PRESCRIPTION_MEDICATION' if setting values for 'NumberOfDrugPrescriptions'.";

        private const string ERROR_MESSAGE_EXISTING_PRESCRIPTION = "ExistingPrescription must be 'false' if not setting values for Coverage type of 'PRESCRIPTION_MEDICATION'.";

        private const string PRESCRIPTION_MEDICATION = "PRESCRIPTION_MEDICATION";

        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is not Questions)
            {
                return new ValidationResult("PrescriptionMedicationValidator must be used on Questions");
            }

            Questions questions = (Questions)value;

            if (questions.NumberOfDrugPrescriptions == "" && questions.CoverageType.Contains(PRESCRIPTION_MEDICATION))
            {
                return new ValidationResult(ERROR_MESSAGE_NO_NUMBER_OF_DRUGS);
            }

            if (questions.NumberOfDrugPrescriptions != "" && !questions.CoverageType.Contains(PRESCRIPTION_MEDICATION))
            {
                return new ValidationResult(ERROR_MESSAGE_NO_COVERAGE_TYPE);
            }

            if (questions.ExistingPrescription && !questions.CoverageType.Contains(PRESCRIPTION_MEDICATION))
            {
                return new ValidationResult(ERROR_MESSAGE_EXISTING_PRESCRIPTION);
            }

            return ValidationResult.Success;
        }
    }
}
