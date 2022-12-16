using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.CriticalIllnessTests
{
    [TestClass]
    public class CriticalIllnessCoverageOptionTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: TRADITIONAL, HIGH_SEVERITY, '', null.";

        private const string TRADITIONAL = "TRADITIONAL";
        private const string HIGH_SEVERITY = "HIGH_SEVERITY";

        [TestMethod]
        public void Invalid_CICoverageAmount_Fails()
        {
            string COVERAGE_OPTION = "asdf";

            ModelValidator.AssertValidatorHasResult(new CriticalIllness()
            {
                coverageOption = COVERAGE_OPTION
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(TRADITIONAL)]
        [DataRow(HIGH_SEVERITY)]
        [DataRow("")]
        public void Valid_DentalCombinedMaximum_Passes(string coverageOption)
        {
            ModelValidator.AssertValidatorNoResult(new CriticalIllness()
            {
                coverageOption = coverageOption,
            });
        }
    }
}



