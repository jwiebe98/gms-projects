using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.LTDTests
{
    [TestClass]
    public class LTDWaitingPeriodTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: 112_DAYS, 119_DAYS, 182_DAYS, '', null.";

        private const string _112_DAYS = "112_DAYS";
        private const string _119_DAYS = "119_DAYS";
        private const string _182_DAYS = "182_DAYS";

        [TestMethod]
        public void Invalid_WaitingPeriod_Fails()
        {
            string WAITING_PERIOD = "asdf";

            ModelValidator.AssertValidatorHasResult(new LongTermDisability()
            {
                waitingPeriod = WAITING_PERIOD
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_112_DAYS)]
        [DataRow(_119_DAYS)]
        [DataRow(_182_DAYS)]
        [DataRow("")]
        public void Valid_SicknessWaitingPeriod_Passes(string waitingPeriod)
        {
            ModelValidator.AssertValidatorNoResult(new LongTermDisability()
            {
                waitingPeriod = waitingPeriod,
            });
        }
    }
}

