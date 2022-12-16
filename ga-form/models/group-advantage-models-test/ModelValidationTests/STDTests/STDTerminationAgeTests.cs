using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.STDTests
{
    [TestClass]
    public class STDTerminationAgeTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: AGE_65, '', null.";

        private const string AGE_65 = "AGE_65";

        [TestMethod]
        public void Invalid_STDTerminationAge_Fails()
        {
            string TERMINATION_AGE = "asdf";

            ModelValidator.AssertValidatorHasResult(new ShortTermDisability()
            {
                terminationAge = TERMINATION_AGE
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(AGE_65)]
        [DataRow("")]
        public void Valid_STDTerminationAge_Passes(string terminationAge)
        {
            ModelValidator.AssertValidatorNoResult(new ShortTermDisability()
            {
                terminationAge = terminationAge,
            });
        }
    }
}



