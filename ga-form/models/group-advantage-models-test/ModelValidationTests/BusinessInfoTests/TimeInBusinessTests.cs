using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.BusinessInfoTests
{
    [TestClass]
    public class TimeInBusinessTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: LESS_THAN_6_MO, SIX_TO_TWELVE, MORE_TWELVE, '', null.";

        [TestMethod]
        public void Invalid_Time_Fails()
        {
            string TIME_IN_BUSINESS = "asdf";

            ModelValidator.AssertValidatorHasResult(
                new BusinessInfo()
                {
                    timeInBusiness = TIME_IN_BUSINESS
                },
                ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("LESS_THAN_6_MO")]
        [DataRow("SIX_TO_TWELVE")]
        [DataRow("MORE_TWELVE")]
        [DataRow("")]
        public void Valid_Time_Passes(string TIME_IN_BUSINESS)
        {
            ModelValidator.AssertValidatorNoResult(
                new BusinessInfo()
                {
                    timeInBusiness = TIME_IN_BUSINESS
                });
        }

    }
}