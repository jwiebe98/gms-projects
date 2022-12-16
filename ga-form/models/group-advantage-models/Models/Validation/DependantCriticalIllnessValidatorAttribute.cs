using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    [AttributeUsage(AttributeTargets.Class,
    AllowMultiple = true, Inherited = true)]
    public sealed class DependantCriticalIllnessValidatorAttribute : ValidationAttribute
    {
        private const string HIGH_SEVERITY = "HIGH_SEVERITY";
        private const string TRADITIONAL = "TRADITIONAL";

        private const string _10000 = "_10000";
        private const string _5000 = "_5000";
        private const string _10000_5000 = "_10000_5000";
        private const string _5000_2500 = "_5000_2500";

        public override string FormatErrorMessage(string _)
        {
            return "HIGH_SEVERITY coverage amount must be '_10000' or '_5000', TRADITIONAL coverage amount must be '_10000_5000' or  '_5000_2500'";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is not DependantCriticalIllness)
            {
                return false;
            }

            DependantCriticalIllness depCI = (DependantCriticalIllness)value;

            string coverageOption = depCI.coverageOption;
            string coverageAmount = depCI.coverageAmount;

            return coverageAmount is "" || coverageOption is "" ||
                (coverageOption is HIGH_SEVERITY ? coverageAmount is not _10000_5000 and not _5000_2500 :
                coverageOption is TRADITIONAL && coverageAmount is not _10000 and not _5000);
        }
    }
}
