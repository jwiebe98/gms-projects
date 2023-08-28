using Individual.HelpMeChoose.Models.Tests.Helpers;

namespace Gmsca.HelpMeChoose.Individual.Models.Tests
{
    [TestClass]
    public class PrescriptionMedicaitonValidatorTests
    {
        private const string ERROR_MESSAGE_NO_NUMBER_OF_DRUGS = "Coverage type of 'PRESCRIPTION_MEDICATION' Must have values set for 'NumberOfDrugPrescriptions'.";

        private const string ERROR_MESSAGE_NO_COVERAGE_TYPE = "CoverageType must contain 'PRESCRIPTION_MEDICATION' if setting values for 'NumberOfDrugPrescriptions'.";

        private const string ERROR_MESSAGE_EXISTING_PRESCRIPTION = "ExistingPrescription must be 'false' if not setting values for Coverage type of 'PRESCRIPTION_MEDICATION'.";

        private const string PRESCRIPTION_MEDICATION = "PRESCRIPTION_MEDICATION";

        [TestMethod]
        public void No_NumberOfDrugs_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                CoverageType = new()
                {
                    PRESCRIPTION_MEDICATION
                }
            },
            ERROR_MESSAGE_NO_NUMBER_OF_DRUGS);
        }

        [TestMethod]
        public void CoverageType_Not_Set_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                NumberOfDrugPrescriptions = "JUST_FOR_EMERGENCIES"
            },
            ERROR_MESSAGE_NO_COVERAGE_TYPE);
        }

        [TestMethod]
        public void Invalid_ExistingPrescription_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                ExistingPrescription = true
            },
            ERROR_MESSAGE_EXISTING_PRESCRIPTION);
        }

        [TestMethod]
        public void Valid_PrecriptionMedicaition_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Questions()
            {
                NumberOfDrugPrescriptions = "JUST_FOR_EMERGENCIES",
                CoverageType = new()
                {
                    PRESCRIPTION_MEDICATION
                },
                ExistingPrescription = true
            });
        }

        [TestMethod]
        public void Valid_Null_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Questions());
        }
    }
}