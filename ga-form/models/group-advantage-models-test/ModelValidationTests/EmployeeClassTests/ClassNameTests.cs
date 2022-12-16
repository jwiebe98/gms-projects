using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeClassTests
{
    [TestClass]
    public class ClassNameTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: A, B, '', null.";

        [TestMethod]
        public void Invalid_ClassName_Fails()
        {
            string CLASS_NAME = "asdf";

            ModelValidator.AssertValidatorHasResult(new EmployeeClass()
            {
                className = CLASS_NAME
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("A")]
        [DataRow("B")]
        [DataRow("")]
        [DataRow(null)]
        public void Valid_ClassName_Passes(string CLASS_NAME)
        {
            ModelValidator.AssertValidatorNoResult(new EmployeeClass()
            {
                className = CLASS_NAME
            });
        }
    }
}