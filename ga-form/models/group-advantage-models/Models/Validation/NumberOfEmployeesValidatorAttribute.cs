using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    public sealed class NumberOfEmployeesValidatorAttribute : ValidationAttribute
    {

        public override string FormatErrorMessage(string _)
        {
            return "Number of Employees must be between 3 and 10.";
        }

        public override bool IsValid(object? value)
        {

            if (value == null)
            {
                return true;
            }

            if (value is not List<EmployeeClass>)
            {
                return false;
            }

            int numberofEmployees = ((List<EmployeeClass>)value).SelectMany(x => x.employees).Count();

            return numberofEmployees is 0 or (<= 10 and >= 3);
        }
    }
}
