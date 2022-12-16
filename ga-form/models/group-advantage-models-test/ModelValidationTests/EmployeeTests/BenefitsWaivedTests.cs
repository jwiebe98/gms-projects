using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeTests
{
    [TestClass]
    public class BenefitsWaivedTests
    {

        private const string ERROR_MESSAGE = "Invalid value 'foo', List can only contain: [HEALTH, DENTAL].";

        [TestMethod]
        public void Invalid_BenefitsWaived_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Employee()
            {
                benefitsWaived = new() { "foo" }
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("HEALTH")]
        [DataRow("DENTAL")]
        [DataRow("HEALTH,DENTAL")]
        public void Valid_WaivedBenefits_Passes(string waivedBenefits)
        {
            ModelValidator.AssertValidatorNoResult(new Employee()
            {
                benefitsWaived = waivedBenefits.Split(",").ToList()
            });
        }

        [TestMethod]
        public void Valid_EmptyWaivedBenefitsList_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Employee() { });
        }
    }
}