using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Gmsca.Group.GA.Models.Validation
{
    public sealed class AssumptionLifeProductValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is not List<EmployeeClass>)
            {
                return new ValidationResult("AssumptionLifeProductValidator must be used on List<EmployeeClass>");
            }

            List<EmployeeClass> employeeClasses = (List<EmployeeClass>)value;

            if (employeeClasses.Count is 0)
            {
                return ValidationResult.Success;
            }

            if (employeeClasses.Any(ec => ec.benefits.healthPlan is null)) { return ValidationResult.Success; }

            int qualifiedEmployees = GetNumberOfQualifiedEmployeesInGroup(employeeClasses);

            int minLives = GetMinLives(employeeClasses);

            return qualifiedEmployees < minLives
                ? new ValidationResult("Group does not meet qualified employee number requirement for Assumption Life products.")
                : ValidationResult.Success;
        }

        private static int GetMinLives(List<EmployeeClass> employeeClasses)
        {
            return GroupHasAssumptionLifeBenefits(employeeClasses) ? 2 : 3;
        }

        private static bool GroupHasAssumptionLifeBenefits(List<EmployeeClass> employeeClasses)
        {
            return employeeClasses.Select(
                    ec => ec.benefits.GetType().GetProperties()
                    .Where(PropertyIsNotHealthOrDental)
                    .Select(pi => pi.GetValue(ec.benefits))
                    .Any(value => value is not null or true)
                    ).Contains(true);
        }

        private static bool PropertyIsNotHealthOrDental(PropertyInfo pi)
        {
            return !pi.Name.Equals("healthPlan") && !pi.Name.Equals("dentalPlan");
        }

        private static int GetNumberOfDisqualifiedEmployeesInClass(EmployeeClass employeeClass)
        {
            return employeeClass.benefits.healthPlan is not null && employeeClass.benefits.dentalPlan is not null
                ? employeeClass.employees.Where(e => e.benefitsWaived.Contains("HEALTH") && e.benefitsWaived.Contains("DENTAL")).Count()
                : employeeClass.benefits.healthPlan is not null && employeeClass.benefits.dentalPlan is null
                ? employeeClass.employees.Where(e => e.benefitsWaived.Contains("HEALTH")).Count()
                : 0;
        }

        private static int GetNumberOfQualifiedEmployeesInGroup(List<EmployeeClass> employeeClasses)
        {
            int numberOfEmployeesInGroup = employeeClasses.SelectMany(x => x.employees).Count();

            int disqualifiedEmployees = employeeClasses.Select(GetNumberOfDisqualifiedEmployeesInClass).Sum();

            return numberOfEmployeesInGroup - disqualifiedEmployees;
        }
    }
}
