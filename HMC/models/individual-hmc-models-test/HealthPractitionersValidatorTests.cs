using Individual.HelpMeChoose.Models.Tests.Helpers;

namespace Gmsca.HelpMeChoose.Individual.Models.Tests
{
    [TestClass]
    public class HealthPractitionersValidatorTests
    {
        private const string ERROR_MESSAGE_NO_HEALTH_CARE_PRACTITIONER_TYPES = "Coverage type of 'HEALTH_PRACTITIONERS' Must have values set for 'HealthCarePractitionerType'.";

        private const string ERROR_MESSAGE_HEALTH_PRACTITONERS_NOT_SET = "CoverageType must contain 'HEALTH_PRACTITIONERS' if setting values for 'HealthCarePractitionerType'.";

        private const string HEALTH_PRACTITIONERS = "HEALTH_PRACTITIONERS";

        [TestMethod]
        public void No_HealthCarePractitionerTypes_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                CoverageType = new()
                {
                    HEALTH_PRACTITIONERS
                }
            },
            ERROR_MESSAGE_NO_HEALTH_CARE_PRACTITIONER_TYPES);
        }

        [TestMethod]
        public void Health_Practitioners_Not_Set_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                HealthCarePractitionerType = new()
                {
                    "CHIROPRACTOR"
                }
            },
            ERROR_MESSAGE_HEALTH_PRACTITONERS_NOT_SET);
        }

        [TestMethod]
        public void Valid_Health_Practitioners_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Questions()
            {
                HealthCarePractitionerType = new()
                {
                    "CHIROPRACTOR"
                },
                CoverageType = new()
                {
                    HEALTH_PRACTITIONERS
                }
            });
        }

        [TestMethod]
        public void Valid_Null_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Questions());
        }
    }
}