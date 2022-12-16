using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.PlanInfoTests
{
    [TestClass]
    public class TimeWithCurrentCarrierTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: LESS_THAN_1_YEAR, ONE_TO_TWO_YEAR, MORE_THAN_2_YEARS, '', null.";

        [TestMethod]
        public void Invalid_Time_Fails()
        {
            string TIME_WITH_CURRENT_CARRIER = "asdf";

            ModelValidator.AssertValidatorHasResult(
                new PlanInfo()
                {
                    timeWithCurrentCarrier = TIME_WITH_CURRENT_CARRIER
                },
                ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("LESS_THAN_1_YEAR")]
        [DataRow("ONE_TO_TWO_YEAR")]
        [DataRow("MORE_THAN_2_YEARS")]
        [DataRow("")]
        [DataRow(null)]
        public void Valid_Time_Passes(string TIME_WITH_CURRENT_CARRIER)
        {
            ModelValidator.AssertValidatorNoResult(
                new PlanInfo()
                {
                    timeWithCurrentCarrier = TIME_WITH_CURRENT_CARRIER
                });
        }

    }
}