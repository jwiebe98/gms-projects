using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.STDTests
{
    [TestClass]
    public class STDAccidentWaitingPeriodTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: 0_DAYS, 7_DAYS, '', null.";

        private const string _0_DAYS = "0_DAYS";
        private const string _7_DAYS = "7_DAYS";

        [TestMethod]
        public void Invalid_AccidentWaitingPeriod_Fails()
        {
            string ACCIDENT_WAITING_PERIOD = "asdf";

            ModelValidator.AssertValidatorHasResult(new ShortTermDisability()
            {
                accidentWaitingPeriod = ACCIDENT_WAITING_PERIOD
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_0_DAYS)]
        [DataRow(_7_DAYS)]
        [DataRow("")]
        public void Valid_AccidentWaitingPeriod_Passes(string accidentWaitingPeriod)
        {
            ModelValidator.AssertValidatorNoResult(new ShortTermDisability()
            {
                accidentWaitingPeriod = accidentWaitingPeriod,
            });
        }
    }
}
