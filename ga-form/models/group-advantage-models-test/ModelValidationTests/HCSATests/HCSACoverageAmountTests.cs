using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.HCSATests
{
    [TestClass]
    public class HCSACoverageAmountTests
    {

        private const string ERROR_MESSAGE = "Health Spending Coverage Amount must be a multiple of $50 and be between $250 and $15,000";

        [DataTestMethod]
        [DataRow(16750)]
        [DataRow(1234)]
        [DataRow(150)]
        public void Invalid_CoverageAmount_Fails(long coverageAmount)
        {
            ModelValidator.AssertValidatorHasResult(new HealthSpending()
            {
                coverageAmount = coverageAmount
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(250)]
        [DataRow(15000)]
        [DataRow(7500)]
        public void Valid_CoverageAmount_Passes(long coverageAmount)
        {
            ModelValidator.AssertValidatorNoResult(new HealthSpending()
            {
                coverageAmount = coverageAmount
            });
        }
    }
}