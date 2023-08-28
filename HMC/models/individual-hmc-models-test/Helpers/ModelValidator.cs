using System.ComponentModel.DataAnnotations;

namespace Individual.HelpMeChoose.Models.Tests.Helpers
{
    public static class ModelValidator
    {
        public static void AssertValidatorNoResult<T>(T data)
        {
            if (data is null)
            {
                Assert.Fail("Test data cannot be null");
            }

            IList<ValidationResult> validatorResult = Validate(data);

            Assert.IsTrue(validatorResult.Count is 0);
        }
        public static void AssertValidatorHasResult<T>(T data, string errorMessage)
        {
            if (data is null)
            {
                Assert.Fail("Test data cannot be null");
            }

            IList<ValidationResult> validatorResult = Validate(data);

            Assert.IsTrue(validatorResult.Any(
                v => v.ErrorMessage is not null &&
                     v.ErrorMessage.Equals(errorMessage))
                );
        }
        private static IList<ValidationResult> Validate(object model)
        {
            List<ValidationResult> validationResults = new();
            ValidationContext ctx = new(model, null, null);
            _ = Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
