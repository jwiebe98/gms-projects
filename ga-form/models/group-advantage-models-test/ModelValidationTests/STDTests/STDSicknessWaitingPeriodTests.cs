using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.STDTests
{
    [TestClass]
    public class STDSicknessWaitingPeriodTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: 7_DAYS, '', null.";

        private const string _7_DAYS = "7_DAYS";

        [TestMethod]
        public void Invalid_SicknessWaitingPeriod_Fails()
        {
            string SICKNESS_WAITING_PERIOD = "asdf";

            ModelValidator.AssertValidatorHasResult(new ShortTermDisability()
            {
                sicknessWaitingPeriod = SICKNESS_WAITING_PERIOD
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_7_DAYS)]
        [DataRow("")]
        public void Valid_SicknessWaitingPeriod_Passes(string sicknessWaitingPeriod)
        {
            ModelValidator.AssertValidatorNoResult(new ShortTermDisability()
            {
                sicknessWaitingPeriod = sicknessWaitingPeriod,
            });
        }
    }
}

