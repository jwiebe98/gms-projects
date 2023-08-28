using Individual.HelpMeChoose.Models.Tests.Helpers;
using System.ComponentModel;

namespace Gmsca.HelpMeChoose.Individual.Models.Tests
{
    [TestClass]
    public class SpouseAgeValidatorTests
    {
        private const string ERROR_MESSAGE_SPOUSE_AGE_NOT_SET = "If Questions.NumberPeopleCovered contains 'SPOUSE' Applicant.SpouseAge must be set.";

        private const string ERROR_MESSAGE_NUMBER_OF_PEOPLE_COVERED_NOT_SPOUSE = "If Applicant.SpouseAge is set Questions.NumberPeopleCovered must contain 'SPOUSE'.";

        private const string HEALTH_PRACTITIONERS = "HEALTH_PRACTITIONERS";

        [TestMethod]
        public void No_SpouseAge_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Quote()
            {
                Questions = new()
                {
                    NumberPeopleCovered = "YOU_YOUR_SPOUSE"
                }
            },
            ERROR_MESSAGE_SPOUSE_AGE_NOT_SET);
        }

        [TestMethod]
        public void No_Spouse_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Quote()
            {
                Applicant = new()
                {
                    SpouseAge = 23
                },

            },
            ERROR_MESSAGE_NUMBER_OF_PEOPLE_COVERED_NOT_SPOUSE);
        }

        [TestMethod]
        public void Valid_Spouse_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                Applicant = new()
                {
                    SpouseAge = 23
                },
                Questions = new()
                {
                    NumberPeopleCovered = "YOU_YOUR_SPOUSE"
                }
            });
        }

        [TestMethod]
        public void Valid_Null_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote());
        }
    }
}