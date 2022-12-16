using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.QuoteTests
{
    [TestClass]
    public class EmailTests
    {

        private const string ERROR_MESSAGE = "The field emailAddress is invalid.";

        [TestMethod]
        public void Invalid_Email_Fails()
        {
            string EMAIL_ADDRESS = "asdf";

            ModelValidator.AssertValidatorHasResult(new ContactInfo()
            {
                emailAddress = EMAIL_ADDRESS
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow("test@test.ca")]
        [DataRow("")]
        [DataRow(null)]
        public void Valid_Email_Passes(string EMAIL_ADDRESS)
        {
            ModelValidator.AssertValidatorNoResult(new ContactInfo()
            {
                emailAddress = EMAIL_ADDRESS
            });
        }
    }
}