using Gmsca.Group.GA.Models.Tests.Helpers;

namespace Gmsca.Group.GA.Models.Tests.ModelValidationTests.EmployeeClassTests
{
    [TestClass]
    public class WaivedBenefitsMinimumTest
    {

        private const string ERROR_MESSAGE = "Group does not meet qualified employee number requirement for Assumption Life products.";

        private static readonly Benefits HEALTH_DENTAL_LIFE_ADD = new()
        {
            healthPlan = "GOLD",
            dentalPlan = new()
            {
                tier = "GOLD",
                combinedYearlyMaximum = "_500"
            },
            lifeInsurance = new()
            {
                coverageAmount = "_10000"
            },
            accidentalDeathAndDismemberment = new()
            {
                coverageAmount = "_10000"
            }
        };

        private static readonly Benefits HEALTH_DENTAL = new()
        {
            healthPlan = "GOLD",
            dentalPlan = new()
            {
                tier = "GOLD",
                combinedYearlyMaximum = "_500"
            },
        };

        private static readonly Benefits NO_BENEFITS = new();

        private static readonly List<Employee> _1_EMPLOYEE_0_WAIVE = new() { new() { type = "COUPLE" } };
        private static readonly List<Employee> _1_EMPLOYEE_1_WAIVE_HEALTH_DENTAL = new() { new() { type = "COUPLE", benefitsWaived = new() { "HEALTH", "DENTAL" } } };
        private static readonly List<Employee> _2_EMPLOYEES_0_WAIVE = new() { new() { type = "COUPLE" }, new() { type = "COUPLE" } };
        private static readonly List<Employee> _2_EMPLOYEES_1_WAIVE_HEALTH = new() { new() { type = "COUPLE", benefitsWaived = new() { "HEALTH" } }, new() { type = "COUPLE" } };
        private static readonly List<Employee> _2_EMPLOYEES_1_WAIVE_DENTAL = new() { new() { type = "COUPLE", benefitsWaived = new() { "HEALTH" } }, new() { type = "COUPLE" } };
        private static readonly List<Employee> _2_EMPLOYEES_1_WAIVE_HEALTH_DENTAL = new() { new() { type = "COUPLE", benefitsWaived = new() { "HEALTH", "DENTAL" } }, new() { type = "COUPLE" } };

        [TestMethod]
        public void ClassA_1Employee_HEALTH_DENTAL_NoWaive_ClassB_2Employee_HEALTH_DENTAL_NoWaive_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = HEALTH_DENTAL,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_0_WAIVE
                    },
                    new(){
                        benefits = HEALTH_DENTAL,
                        className = "B",
                        employees = _2_EMPLOYEES_0_WAIVE
                    }
                }
            });
        }

        [TestMethod]
        public void ClassA_1Employee_HEALTH_DENTAL_NoWaive_ClassB_2Employee_HEALTH_DENTAL_1Waive_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = HEALTH_DENTAL,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_0_WAIVE
                    },
                    new(){
                        benefits = HEALTH_DENTAL,
                        className = "B",
                        employees = _2_EMPLOYEES_1_WAIVE_HEALTH_DENTAL
                    }
                }
            }, ERROR_MESSAGE);
        }

        [TestMethod]
        public void ClassA_1Employee_HEALTH_DENTAL_NoWaive_ClassB_2Employee_HEALTH_DENTAL_1Waive_HEALTHONLY_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = HEALTH_DENTAL,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_0_WAIVE
                    },
                    new(){
                        benefits = HEALTH_DENTAL,
                        className = "B",
                        employees = _2_EMPLOYEES_1_WAIVE_HEALTH
                    }
                }
            });
        }

        [TestMethod]
        public void ClassA_1Employee_HEALTH_DENTAL_NoWaive_ClassB_2Employee_HEALTH_DENTAL_1Waive_DENTALONLY_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = HEALTH_DENTAL,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_0_WAIVE
                    },
                    new(){
                        benefits = HEALTH_DENTAL,
                        className = "B",
                        employees = _2_EMPLOYEES_1_WAIVE_DENTAL
                    }
                }
            });
        }

        [TestMethod]
        public void ClassA_1Employee_HEALTH_LIFE_ADD_NoWaive_ClassB_2Employee_HEALTH_LIFE_ADD_NoWaive_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = HEALTH_DENTAL_LIFE_ADD,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_0_WAIVE
                    },
                    new(){
                        benefits = HEALTH_DENTAL_LIFE_ADD,
                        className = "B",
                        employees = _2_EMPLOYEES_0_WAIVE
                    }
                }
            });
        }

        [TestMethod]
        public void ClassA_1Employee_HEALTH_DENTAL_LIFE_ADD_1WaiveHealthAndDental_ClassB_2Employee_HEALTH_DENTAL_LIFE_ADD_1WaiveHealthAndDental_Fails()
        {
            ModelValidator.AssertValidatorHasResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = HEALTH_DENTAL_LIFE_ADD,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_1_WAIVE_HEALTH_DENTAL
                    },
                    new(){
                        benefits = HEALTH_DENTAL_LIFE_ADD,
                        className = "B",
                        employees = _2_EMPLOYEES_1_WAIVE_HEALTH_DENTAL
                    }
                }
            }, ERROR_MESSAGE);
        }

        [TestMethod]
        public void ClassA_1Employee_HEALTH_LIFE_ADD_1Waive_ClassB_2Employee_NO_BENEFITS_NoWaive_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = HEALTH_DENTAL_LIFE_ADD,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_1_WAIVE_HEALTH_DENTAL
                    },
                    new(){
                        benefits = NO_BENEFITS,
                        className = "B",
                        employees = _2_EMPLOYEES_0_WAIVE
                    }
                }
            });
        }

        [TestMethod]
        public void ClassA_1Employee_NO_BENEFITS_0Waive_ClassB_2Employee_HEALTH_DENTAL_1Waive_HealthAndDental_Passes()
        {
            ModelValidator.AssertValidatorNoResult(new Quote()
            {
                classes = new()
                {
                    new(){
                        benefits = NO_BENEFITS,
                        isEmployerClass = true,
                        className = "A",
                        employees = _1_EMPLOYEE_0_WAIVE
                    },
                    new(){
                        benefits = HEALTH_DENTAL,
                        className = "B",
                        employees = _2_EMPLOYEES_1_WAIVE_HEALTH_DENTAL
                    }
                }
            });
        }
    }
}