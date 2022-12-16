using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.STDTests
{
    [TestClass]
    public class STDBenefitDurationTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: 16_WEEKS, 17_WEEKS, 26_WEEKS, '', null.";

        private const string _16_WEEKS = "16_WEEKS";
        private const string _17_WEEKS = "17_WEEKS";
        private const string _26_WEEKS = "26_WEEKS";

        [TestMethod]
        public void Invalid_BenefitDuration_Fails()
        {
            string BENEFIT_DURATION = "asdf";

            ModelValidator.AssertValidatorHasResult(new ShortTermDisability()
            {
                benefitDuration = BENEFIT_DURATION
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_16_WEEKS)]
        [DataRow(_17_WEEKS)]
        [DataRow(_26_WEEKS)]
        [DataRow("")]
        public void Valid_BenefitDuration_Passes(string benefitDuration)
        {
            ModelValidator.AssertValidatorNoResult(new ShortTermDisability()
            {
                benefitDuration = benefitDuration,
            });
        }
    }
}



