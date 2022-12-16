using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    [AttributeUsage(AttributeTargets.Class,
    AllowMultiple = true, Inherited = true)]
    public sealed class CriticalIllnessCoverageAmountAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string _)
        {
            return "If there are less than 6 employees in the class, the critical illness coverage amount must be 10000$";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is not EmployeeClass)
            {
                return false;
            }

            EmployeeClass employeeClass = (EmployeeClass)value;

            return employeeClass.benefits.criticalIllness is null ||
                employeeClass.employees.Count >= 6 ||
                employeeClass.benefits.criticalIllness.coverageAmount is not "_25000";
        }
    }
}
