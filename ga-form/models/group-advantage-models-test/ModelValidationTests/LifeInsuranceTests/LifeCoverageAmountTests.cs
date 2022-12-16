using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.LifeInsuranceTests
{
    [TestClass]
    public class LifeCoverageAmountTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: _10000, _25000, _50000, _1XSALARY, '', null.";

        private const string _10000 = "_10000";
        private const string _25000 = "_25000";
        private const string _50000 = "_50000";
        private const string _1xSalary = "_1XSALARY";

        [TestMethod]
        public void Invalid_LifeCoverageAmount_Fails()
        {
            string COVERAGE_AMOUNT = "asdf";

            ModelValidator.AssertValidatorHasResult(new LifeInsurance()
            {
                coverageAmount = COVERAGE_AMOUNT
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_10000)]
        [DataRow(_25000)]
        [DataRow(_50000)]
        [DataRow(_1xSalary)]
        [DataRow("")]
        public void Valid_LifeCoverageAmount_Passes(string coverageAmount)
        {
            ModelValidator.AssertValidatorNoResult(new LifeInsurance()
            {
                coverageAmount = coverageAmount,
            });
        }
    }
}

