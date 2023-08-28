using Individual.HelpMeChoose.Models.Tests.Helpers;

namespace Gmsca.HelpMeChoose.Individual.Models.Tests
{
    [TestClass]
    public class MentalHealthValidatorTests
    {
        private const string ERROR_MESSAGE_NO_FREQUENCY = "Coverage type of 'MENTAL_HEALTH_SUPPORT' Must have values set for 'FrequencyOfMentalHealthVisits'.";

        private const string ERROR_MESSAGE_NO_COVERAGE_TYPE = "CoverageType must contain 'MENTAL_HEALTH_SUPPORT' if setting values for 'FrequencyOfMentalHealthVisits'.";

        private const string MENTAL_HEALTH_SUPPORT = "MENTAL_HEALTH_SUPPORT";

        [TestMethod]
        public void No_Frequency_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                CoverageType = new()
                {
                    MENTAL_HEALTH_SUPPORT
                }
            },
            ERROR_MESSAGE_NO_FREQUENCY);
        }

        [TestMethod]
        public void Mental_Health_Support_Not_Set_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                FrequencyOfMentalHealthVisits = "4_TO_8"
            },
            ERROR_MESSAGE_NO_COVERAGE_TYPE);
        }

        [TestMethod]
        public void Valid_Mental_Health_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Questions()
            {
                FrequencyOfMentalHealthVisits = "4_TO_8",
                CoverageType = new()
                {
                    MENTAL_HEALTH_SUPPORT
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