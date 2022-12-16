using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.CriticalIllnessTests
{
    [TestClass]
    public class DependantCriticalIllnessTests
    {

        private const string ERROR_MESSAGE = "HIGH_SEVERITY coverage amount must be '_10000' or '_5000', TRADITIONAL coverage amount must be '_10000_5000' or  '_5000_2500'";

        private const string TRADITIONAL = "TRADITIONAL";
        private const string HIGH_SEVERITY = "HIGH_SEVERITY";

        private const string _10000 = "_10000";
        private const string _10000_5000 = "_10000_5000";
        private const string _5000 = "_5000";
        private const string _5000_2500 = "_5000_2500";

        private const string EMPTY = "";

        [DataTestMethod]
        [DataRow(TRADITIONAL, _10000_5000)]
        [DataRow(TRADITIONAL, _5000_2500)]
        [DataRow(HIGH_SEVERITY, _10000)]
        [DataRow(HIGH_SEVERITY, _5000)]
        [DataRow(EMPTY, EMPTY)]
        [DataRow(EMPTY, _5000)]
        [DataRow(HIGH_SEVERITY, EMPTY)]
        public void Valid_CoverageOptionsAndAmountsPass(string coverageOption, string coverageAmount)
        {
            ModelValidator.AssertValidatorNoResult(new DependantCriticalIllness()
            {
                coverageAmount = coverageAmount,
                coverageOption = coverageOption
            });
        }

        [DataTestMethod]
        [DataRow(TRADITIONAL, _10000)]
        [DataRow(TRADITIONAL, _5000)]
        [DataRow(HIGH_SEVERITY, _10000_5000)]
        [DataRow(HIGH_SEVERITY, _5000_2500)]
        public void Invalid_CoverageOptionsAndAmountsFail(string coverageOption, string coverageAmount)
        {
            ModelValidator.AssertValidatorHasResult(new DependantCriticalIllness()
            {
                coverageAmount = coverageAmount,
                coverageOption = coverageOption
            }, ERROR_MESSAGE);
        }

    }
}
