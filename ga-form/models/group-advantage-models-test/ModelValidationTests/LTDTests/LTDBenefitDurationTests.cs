using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.LTDTests
{
    [TestClass]
    public class LTDBenefitDurationTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: 5_YEARS, AGE_65, '', null.";

        private const string _5_YEARS = "5_YEARS";
        private const string AGE_65 = "AGE_65";

        [TestMethod]
        public void Invalid_BenefitDuration_Fails()
        {
            string BENEFIT_DURATION = "asdf";

            ModelValidator.AssertValidatorHasResult(new LongTermDisability()
            {
                benefitDuration = BENEFIT_DURATION
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_5_YEARS)]
        [DataRow(AGE_65)]
        [DataRow("")]
        public void Valid_BenefitDuration_Passes(string benefitDuration)
        {
            ModelValidator.AssertValidatorNoResult(new LongTermDisability()
            {
                benefitDuration = benefitDuration,
            });
        }
    }
}



