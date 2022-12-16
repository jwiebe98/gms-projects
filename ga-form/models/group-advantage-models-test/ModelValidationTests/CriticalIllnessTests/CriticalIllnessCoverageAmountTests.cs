using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.CriticalIllnessTests
{
    [TestClass]
    public class CriticalIllnessCoverageAmountTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: _10000, _25000, '', null.";

        private const string _10000 = "_10000";
        private const string _25000 = "_25000";

        [TestMethod]
        public void Invalid_CICoverageAmount_Fails()
        {
            string COVERAGE_AMOUNT = "asdf";

            ModelValidator.AssertValidatorHasResult(new CriticalIllness()
            {
                coverageAmount = COVERAGE_AMOUNT
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_10000)]
        [DataRow(_25000)]
        [DataRow("")]
        public void Valid_DentalCombinedMaximum_Passes(string coverageAmount)
        {
            ModelValidator.AssertValidatorNoResult(new CriticalIllness()
            {
                coverageAmount = coverageAmount,
            });
        }
    }
}
