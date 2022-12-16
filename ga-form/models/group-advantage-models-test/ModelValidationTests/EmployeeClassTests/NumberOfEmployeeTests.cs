using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeClassTests
{
    [TestClass]
    public class NumberOfEmployeeTests
    {

        private const string ERROR_MESSAGE = "Number of Employees must be between 3 and 10.";

        [TestMethod]
        public void Zero_Employees_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote());
        }

        [TestMethod]
        public void Two_Employees_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Quote()
            {
                classes = new()
                {
                    new() { employees = new(){ new() } },
                    new() { employees = new(){ new() } }
                }
            },
            ERROR_MESSAGE);
        }

        [TestMethod]
        public void Eleven_Employees_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Quote()
            {
                classes = new()
                {
                    new() { employees = new(){ new(), new(), new(), new(), new(), new() } },
                    new() { employees = new(){ new(), new(), new(), new(), new() } }
                }
            },
            ERROR_MESSAGE);
        }

        [TestMethod]
        public void Three_Employees_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new()
                {
                    new() { employees = new(){ new(), new() } },
                    new() { employees = new(){ new() } }
                }
            });
        }
        [TestMethod]
        public void Ten_Employees_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new(){
                    new() { employees = new(){ new(), new(), new(), new(), new() } },
                    new() { employees = new(){ new(), new(), new(), new(), new() } }
                }
            });
        }
    }
}