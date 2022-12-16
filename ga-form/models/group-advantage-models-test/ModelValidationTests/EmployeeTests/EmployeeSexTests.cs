using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeTests
{
    [TestClass]
    public class EmployeeTypeTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: SINGLE, COUPLE, FAMILY, '', null.";

        [TestMethod]
        public void Invalid_EmployeeType_Fails()
        {
            string EMPLOYEE_TYPE = "asdf";

            ModelValidator.AssertValidatorHasResult(new Employee()
            {
                type = EMPLOYEE_TYPE
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("SINGLE")]
        [DataRow("COUPLE")]
        [DataRow("FAMILY")]
        [DataRow("")]
        public void Valid_EmployeeType_Passes(string EMPLOYEE_TYPE)
        {
            ModelValidator.AssertValidatorNoResult(new Employee()
            {
                type = EMPLOYEE_TYPE
            });
        }
    }
}