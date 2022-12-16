using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.STDTests
{
    [TestClass]
    public class STDWaitingPeriodTest
    {
        private const string _0_DAYS = "0_DAYS";
        private const string _7_DAYS = "7_DAYS";

        private const string EMPTY = "";

        [DataTestMethod]
        [DataRow(_7_DAYS, _7_DAYS, _7_DAYS)]
        [DataRow(_7_DAYS, _0_DAYS, _7_DAYS)]
        [DataRow(_7_DAYS, _7_DAYS, _0_DAYS)]
        [DataRow(_7_DAYS, _0_DAYS, _0_DAYS)]
        [DataRow(EMPTY, _0_DAYS, _0_DAYS)]

        public void Valid_WaitingPeriods_Pass(string sicknessWaitingPeriod, string hospitalizationWaitingPeriod, string accidentWaitingPeriod)
        {
            ModelValidator.AssertValidatorNoResult(new ShortTermDisability()
            {
                sicknessWaitingPeriod = sicknessWaitingPeriod,
                hospitalizationWaitingPeriod = hospitalizationWaitingPeriod,
                accidentWaitingPeriod = accidentWaitingPeriod
            });
        }
    }
}

