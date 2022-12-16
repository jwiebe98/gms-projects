using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeTests
{
    [TestClass]
    public class EmployeeWaivedBenefitTests
    {

        private const string ERROR_MESSAGE = "Single employees cannot waive benefits.";

        [DataTestMethod]
        [DataRow("HEALTH")]
        [DataRow("DENTAL")]
        [DataRow("HEALTH,DENTAL")]
        public void Invalid_EmployeeType_Fails(string BENEFITS_WAIVED)
        {
            ModelValidator.AssertValidatorHasResult(new Employee()
            {
                benefitsWaived = BENEFITS_WAIVED.Split(",").ToList(),
                type = "SINGLE"
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("HEALTH", "FAMILY")]
        [DataRow("DENTAL", "FAMILY")]
        [DataRow("HEALTH,DENTAL", "FAMILY")]
        [DataRow("HEALTH", "COUPLE")]
        [DataRow("DENTAL", "COUPLE")]
        [DataRow("HEALTH,DENTAL", "COUPLE")]
        public void Valid_Waived_Benefits_Passes(string BENEFITS_WAIVED, string TYPE)
        {
            ModelValidator.AssertValidatorNoResult(new Employee()
            {
                benefitsWaived = BENEFITS_WAIVED.Split(",").ToList(),
                type = TYPE
            });
        }

        [TestMethod]
        public void Single_NoWaive_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Employee()
            {
                type = "SINGLE"
            });
        }

    }
}