using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.BenefitsTests
{
    [TestClass]
    public class HealthPlanTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: SILVER, GOLD, PLATINUM, '', null.";

        [TestMethod]
        public void Invalid_HealthPlan_Fails()
        {
            string HEALTH_PLAN = "asdf";

            ModelValidator.AssertValidatorHasResult(new Benefits()
            {
                healthPlan = HEALTH_PLAN
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("SILVER")]
        [DataRow("GOLD")]
        [DataRow("PLATINUM")]
        [DataRow("")]
        public void Valid_HealthPlan_Passes(string HEALTH_PLAN)
        {
            ModelValidator.AssertValidatorNoResult(new Benefits()
            {
                healthPlan = HEALTH_PLAN
            });
        }
    }
}