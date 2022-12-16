using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.STDTests
{
    [TestClass]
    public class STDBenefitAmountTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: MAXIMUM, EI_MAXIMUM, '', null.";

        private const string MAXIMUM = "MAXIMUM";
        private const string EI_MAXIMUM = "EI_MAXIMUM";

        [TestMethod]
        public void Invalid_BenefitAmount_Fails()
        {
            string BENEFIT_AMOUNT = "asdf";

            ModelValidator.AssertValidatorHasResult(new ShortTermDisability()
            {
                benefitAmount = BENEFIT_AMOUNT
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(MAXIMUM)]
        [DataRow(EI_MAXIMUM)]
        [DataRow("")]
        public void Valid_BenefitAmount_Passes(string benefitAmount)
        {
            ModelValidator.AssertValidatorNoResult(new ShortTermDisability()
            {
                benefitAmount = benefitAmount,
            });
        }
    }
}


