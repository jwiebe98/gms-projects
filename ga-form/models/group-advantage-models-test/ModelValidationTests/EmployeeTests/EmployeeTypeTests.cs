using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeTests
{
    [TestClass]
    public class EmployeeSexTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: MALE, FEMALE, '', null.";

        [TestMethod]
        public void Invalid_EmployeeSex_Fails()
        {
            string EMPLOYEE_SEX = "asdf";

            ModelValidator.AssertValidatorHasResult(new Employee()
            {
                sex = EMPLOYEE_SEX
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("MALE")]
        [DataRow("FEMALE")]
        [DataRow("")]
        public void Valid_EmployeeSex_Passes(string EMPLOYEE_SEX)
        {
            ModelValidator.AssertValidatorNoResult(new Employee()
            {
                sex = EMPLOYEE_SEX
            });
        }
    }
}