using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeTests
{
    [TestClass]
    public class EmployeeAgeTests
    {

        private const string ERROR_MESSAGE = "Employee must be between the ages of 18 and 65.";

        [DataTestMethod]
        [DataRow(17)]
        [DataRow(66)]
        public void Invalid_EmployeeType_Fails(int EMPLOYEE_AGE)
        {
            ModelValidator.AssertValidatorHasResult(new Employee()
            {
                age = EMPLOYEE_AGE
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(18)]
        [DataRow(42)]
        [DataRow(65)]
        public void Valid_EmployeeType_Passes(int EMPLOYEE_AGE)
        {
            ModelValidator.AssertValidatorNoResult(new Employee()
            {
                age = EMPLOYEE_AGE
            });
        }
    }
}