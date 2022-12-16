using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.DentalTests
{
    [TestClass]
    public class DentalTierTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: SILVER, GOLD, PLATINUM, '', null.";

        private const string SILVER = "SILVER";
        private const string GOLD = "GOLD";
        private const string PLATINUM = "PLATINUM";

        [TestMethod]
        public void Invalid_DentalTier_Fails()
        {
            string DENTAL_TEIR = "asdf";

            ModelValidator.AssertValidatorHasResult(new DentalPlan()
            {
                tier = DENTAL_TEIR
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(SILVER)]
        [DataRow(GOLD)]
        [DataRow(PLATINUM)]
        [DataRow("")]
        public void Valid_DentalTier_Passes(string tier)
        {
            ModelValidator.AssertValidatorNoResult(new DentalPlan()
            {
                tier = tier,
            });
        }
    }
}
