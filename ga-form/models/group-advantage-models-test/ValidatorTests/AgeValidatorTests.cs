using Gmsca.Group.GA.Models.Tests.Helpers;
using Gmsca.Group.GA.Models.Validation;

namespace Gmsca.Group.GA.Models.Tests.ValidatorTests
{
    [TestClass]
    public class AgeValidatorTests
    {

        private const string ERROR_MESSAGE = "Employee must be between the ages of 18 and 65.";

        private class ValidAge
        {
            [AgeValidator]
            public int age { get; set; }
        }

        private class InValidAgeStringField
        {
            [AgeValidator]
            public string age { get; set; } = string.Empty;
        }

        [DataTestMethod]
        [DataRow(18)]
        [DataRow(65)]
        public void Valid_Ages_Pass(int AGE)
        {
            ModelValidator.AssertValidatorNoResult(new ValidAge() { age = AGE });
        }

        [DataTestMethod]
        [DataRow(17)]
        [DataRow(66)]
        public void Invalid_Ages_Fail(int AGE)
        {
            ModelValidator.AssertValidatorHasResult(new ValidAge() { age = AGE }, ERROR_MESSAGE);
        }

        [TestMethod]
        public void Invalid_Field_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new InValidAgeStringField() { age = "asdf" }, ERROR_MESSAGE);
        }



    }
}
