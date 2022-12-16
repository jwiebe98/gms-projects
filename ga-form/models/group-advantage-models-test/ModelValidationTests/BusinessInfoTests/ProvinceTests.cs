using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.BusinessInfoTests
{
    [TestClass]
    public class ProvinceTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: BC, AB, SK, MB, ON, NS, PE, NL, YK, NT, '', null.";

        [DataTestMethod]
        [DataRow("BC")]
        [DataRow("AB")]
        [DataRow("SK")]
        [DataRow("MB")]
        [DataRow("ON")]
        [DataRow("NS")]
        [DataRow("PE")]
        [DataRow("NL")]
        [DataRow("YK")]
        [DataRow("NT")]
        [DataRow("")]
        [DataRow(null)]
        public void Province_Success(string province)
        {
            ModelValidator.AssertValidatorNoResult(
                new BusinessInfo()
                {
                    province = province
                });
        }

        [DataTestMethod]
        [DataRow("asdf")]
        public void Province_Fail(string province)
        {
            ModelValidator.AssertValidatorHasResult(
                new BusinessInfo()
                {
                    province = province
                },
                ERROR_MESSAGE);
        }
    }
}
