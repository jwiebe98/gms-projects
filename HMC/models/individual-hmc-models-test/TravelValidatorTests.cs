using Individual.HelpMeChoose.Models.Tests.Helpers;

namespace Gmsca.HelpMeChoose.Individual.Models.Tests
{
    [TestClass]
    public class TravelValidatorTests
    {
        private const string ERROR_MESSAGE_NO_TRAVEL_DURATION = "Coverage type of 'TRAVEL' Must have values set for 'TravelDuration'.";

        private const string ERROR_MESSAGE_NO_COVERAGE_TYPE = "CoverageType must contain 'TRAVEL' if setting values for 'TravelDuration'.";

        private const string TRAVEL = "TRAVEL";

        [TestMethod]
        public void No_TravelDuration_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                CoverageType = new()
                {
                    TRAVEL
                }
            },
            ERROR_MESSAGE_NO_TRAVEL_DURATION);
        }

        [TestMethod]
        public void CoverageType_Not_Set_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Questions()
            {
                TravelDuration = "LESS_THAN_ONE_WEEK"
            },
            ERROR_MESSAGE_NO_COVERAGE_TYPE);
        }

        [TestMethod]
        public void Valid_Travel_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Questions()
            {
                TravelDuration = "LESS_THAN_ONE_WEEK",
                CoverageType = new()
                {
                    TRAVEL
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