using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    public sealed class EmployerClassValidatorAttribute : ValidationAttribute
    {

        public override string FormatErrorMessage(string _)
        {
            return "The Employer Class must be class A and have 1 employee who is marked as the employer.";
        }

        public override bool IsValid(object? value)
        {

            if (value == null)
            {
                return true;
            }

            if (value is not EmployeeClass)
            {
                return false;
            }

            EmployeeClass employeeClass = (EmployeeClass)value;

            bool hasEmployer = employeeClass.employees.FindAll(e => e.isEmployer).Count > 0;

            bool isEmployer = employeeClass.employees.FindAll(e => e.isEmployer).Count == 1;

            bool isClassA = employeeClass.className == "A";

            bool isEmployerClass = employeeClass.isEmployerClass;

            return (isClassA && !isEmployerClass && !isEmployer && !hasEmployer)
                || ((!isClassA || (isEmployerClass && isEmployer && hasEmployer))
                && (isClassA || (!isEmployerClass && !isEmployer && !hasEmployer)));
        }
    }
}
