using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.QuoteTests
{
    [TestClass]
    public class PhoneNumberTests
    {
        private const string ERROR_MESSAGE = "Not a valid phone number";

        [DataTestMethod]
        [DataRow("1234567890")]
        [DataRow("1(306)1234567")]
        [DataRow("13061234567")]
        [DataRow("+1(306)-123-4567")]
        [DataRow("+1(306).123.4567")]
        [DataRow("")]
        public void Valid_CPhoneNumbersPass(string phoneNumber)
        {
            ModelValidator.AssertValidatorNoResult(new ContactInfo()
            {
                phoneNumber = phoneNumber
            });
        }

        [DataTestMethod]
        [DataRow("123abc")]
        public void Invalid_CoverageOptionsAndAmountsFail(string phoneNumber)
        {
            ModelValidator.AssertValidatorHasResult(new ContactInfo()
            {
                phoneNumber = phoneNumber
            }, ERROR_MESSAGE);
        }
    }
}
