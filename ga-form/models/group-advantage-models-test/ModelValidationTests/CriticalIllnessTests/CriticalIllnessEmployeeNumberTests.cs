using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.CriticalIllnessTests
{
    [TestClass]
    public class CriticalIllnessEmployeeNumberTests
    {

        private const string ERROR_MESSAGE = "If there are less than 6 employees in the class, the critical illness coverage amount must be 10000$";

        private static List<Employee> CreateListOfEmployees(int numberOfEmployees)
        {
            return Enumerable.Repeat(new Employee(), numberOfEmployees).ToList();
        }

        [DataTestMethod]
        [DataRow(6, "_10000")]
        [DataRow(5, "_10000")]
        [DataRow(6, "_25000")]
        public void Valid_CoverageOptionsAndEmployeesPass(int numberOfEmployees, string coverageAmount)
        {
            ModelValidator.AssertValidatorNoResult(new EmployeeClass()
            {
                employees = CreateListOfEmployees(numberOfEmployees),
                benefits = new()
                {
                    criticalIllness = new()
                    {
                        coverageAmount = coverageAmount
                    }
                }
            });
        }

        [DataTestMethod]
        [DataRow(5, "_25000")]
        public void Invalid_CoverageOptionsAndEmployeesFail(int numberOfEmployees, string coverageAmount)
        {
            ModelValidator.AssertValidatorHasResult(
                new EmployeeClass()
                {
                    employees = CreateListOfEmployees(numberOfEmployees),
                    benefits = new()
                    {
                        criticalIllness = new()
                        {
                            coverageAmount = coverageAmount
                        }
                    }
                },
                ERROR_MESSAGE
            );
        }

        [TestMethod]
        public void NullCriticalIllnessAndNoEmployeesPasses()
        {
            ModelValidator.AssertValidatorNoResult(new EmployeeClass());
        }
    }
}
