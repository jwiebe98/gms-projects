using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    public sealed class WaivedBenefitValidatorAttribute : ValidationAttribute
    {

        public override string FormatErrorMessage(string _)
        {
            return "Single employees cannot waive benefits.";
        }

        public override bool IsValid(object? value)
        {

            if (value == null)
            {
                return true;
            }

            if (value is not Employee)
            {
                return false;
            }

            Employee employee = (Employee)value;

            return employee.type != "SINGLE" || employee.benefitsWaived.Count == 0;
        }
    }
}

