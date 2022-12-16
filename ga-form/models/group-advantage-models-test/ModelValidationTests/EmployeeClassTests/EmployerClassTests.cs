using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeClassTests
{
    [TestClass]
    public class EmployerClassTests
    {

        private const string ERROR_MESSAGE = "The Employer Class must be class A and have 1 employee who is marked as the employer.";

        [TestMethod]
        public void Zero_Employees_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new EmployeeClass());
        }

        [TestMethod]
        public void One_Employee_ClassA_IsEmployerClass_IsEmployer_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new EmployeeClass()
            {
                employees = new() { new() { isEmployer = true } },
                isEmployerClass = true,
                className = "A"
            });
        }

        [TestMethod]
        public void One_Employee_ClassB_IsEmployerClass_IsEmployer_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new EmployeeClass()
            {
                employees = new() { new() { isEmployer = true } },
                isEmployerClass = true,
                className = "B"
            },
            ERROR_MESSAGE);
        }

        [TestMethod]
        public void One_Employee_ClassA_IsNotEmployerClass_IsEmployer_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new EmployeeClass()
            {
                employees = new() { new() { isEmployer = true } },
                isEmployerClass = false,
                className = "A"
            },
            ERROR_MESSAGE);
        }

        [TestMethod]
        public void Three_Employees_ClassA_IsEmployerClass_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new EmployeeClass()
            {
                employees = new() { new() { isEmployer = true }, new() { isEmployer = true }, new() { isEmployer = true } },
                className = "A",
                isEmployerClass = true
            },
            ERROR_MESSAGE);
        }

        [TestMethod]
        public void Three_Employees_ClassA_IsNotEmployerClass_IsNotEmployer_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new EmployeeClass()
            {
                employees = new() { new(), new(), new() },
                className = "A",
                isEmployerClass = false
            });
        }

        [TestMethod]
        public void Three_Employees_ClassB_IsNotEmployerClass_IsNotEmployer_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new EmployeeClass()
            {
                employees = new() { new(), new(), new() },
                className = "B",
                isEmployerClass = false
            });
        }

        [TestMethod]
        public void One_Employee_ClassA_IsEmployerClass_IsNotEmployer_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new EmployeeClass()
            {
                employees = new() { new() { isEmployer = false } },
                className = "A",
                isEmployerClass = true
            },
            ERROR_MESSAGE);
        }

        [TestMethod]
        public void Three_Employee_ClassB_IsNotEmployerClass_IsEmployer_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new EmployeeClass()
            {
                employees = new() { new() { isEmployer = true }, new() { isEmployer = true }, new() { isEmployer = true } },
                className = "B",
                isEmployerClass = false
            },
            ERROR_MESSAGE);
        }

    }
}