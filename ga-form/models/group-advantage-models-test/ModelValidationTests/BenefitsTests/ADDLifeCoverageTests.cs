using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.BenefitsTests
{
    [TestClass]
    public class ADDLifeCoverageTests
    {

        private const string ERROR_MESSAGE = "'accidentalDeathAndDismemberment.coverageAmount' and 'lifeInsurance.coverageAmount' do not match.";

        [DataTestMethod]
        [DataRow("_10000", "_10000")]
        [DataRow("_25000", "_25000")]
        [DataRow("_50000", "_50000")]
        [DataRow("_1xSalary", "_1xSalary")]
        [DataRow("", "_25000")]
        [DataRow("_25000", "")]
        public void Valid_CoverageOptionCombinationsPass(string addCoverage, string lifeCoverage)
        {
            ModelValidator.AssertValidatorNoResult(
                new Benefits()
                {
                    accidentalDeathAndDismemberment = new()
                    {
                        coverageAmount = addCoverage
                    },
                    lifeInsurance = new()
                    {
                        coverageAmount = lifeCoverage
                    }
                }
            );
        }

        [DataTestMethod]
        [DataRow("_10000", "_25000")]
        [DataRow("_25000", "_10000")]
        public void Invalid_CoverageOptionCombinationsFail(string addCoverage, string lifeCoverage)
        {
            ModelValidator.AssertValidatorHasResult(
                new Benefits()
                {
                    accidentalDeathAndDismemberment = new()
                    {
                        coverageAmount = addCoverage
                    },
                    lifeInsurance = new()
                    {
                        coverageAmount = lifeCoverage
                    }
                },
                ERROR_MESSAGE
            );
        }
    }
}
