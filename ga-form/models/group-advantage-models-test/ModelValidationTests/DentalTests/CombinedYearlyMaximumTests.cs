using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.DentalTests
{
    [TestClass]
    public class CombinedYearlyMaximumTests
    {

        private const string ERROR_MESSAGE = "Please enter one of the allowable values: _500, _1000, _1500, _2000, '', null.";

        private const string _500 = "_500";
        private const string _1000 = "_1000";
        private const string _1500 = "_1500";
        private const string _2000 = "_2000";

        [TestMethod]
        public void Invalid_DentalCombinedMaximum_Fails()
        {
            string YEARLY_MAX = "asdf";

            ModelValidator.AssertValidatorHasResult(new DentalPlan()
            {
                combinedYearlyMaximum = YEARLY_MAX
            },
            ERROR_MESSAGE);
        }

        [DataTestMethod]
        [DataRow(_500)]
        [DataRow(_1000)]
        [DataRow(_1500)]
        [DataRow(_2000)]
        [DataRow("")]
        public void Valid_DentalCombinedMaximum_Passes(string combinedYearlyMaximum)
        {
            ModelValidator.AssertValidatorNoResult(new DentalPlan()
            {
                combinedYearlyMaximum = combinedYearlyMaximum,
            });
        }
    }
}

