using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.LifeInsuranceTests
{
    [TestClass]
    public class DepLifeCoverageAmountTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: _5000_2500, _10000_5000, '', null.";

        private const string _5000_2500 = "_5000_2500";
        private const string _10000_5000 = "_10000_5000";

        [TestMethod]
        public void Invalid_LifeCoverageAmount_Fails()
        {
            string COVERAGE_AMOUNT = "asdf";

            ModelValidator.AssertValidatorHasResult(new DependantLifeInsurance()
            {
                coverageAmount = COVERAGE_AMOUNT
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_5000_2500)]
        [DataRow(_10000_5000)]
        [DataRow("")]
        public void Valid_LifeCoverageAmount_Passes(string coverageAmount)
        {
            ModelValidator.AssertValidatorNoResult(new DependantLifeInsurance()
            {
                coverageAmount = coverageAmount,
            });
        }
    }
}


