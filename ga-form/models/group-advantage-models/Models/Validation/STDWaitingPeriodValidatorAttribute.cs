using System.ComponentModel.DataAnnotations;

namespace Gmsca.Group.GA.Models.Validation
{
    [AttributeUsage(AttributeTargets.Class,
    AllowMultiple = true, Inherited = true)]
    public sealed class STDWaitingPeriodValidatorAttribute : ValidationAttribute
    {

        private ShortTermDisability? _std;

        private const string _0_DAYS = "0_DAYS";
        private const string _7_DAYS = "7_DAYS";

        private const string EMPTY = "";

        public override string FormatErrorMessage(string _)
        {
            return $"Short Term Disability sickness waiting period of {_std?.sicknessWaitingPeriod} must have hospitalization and accident waiting periods set to 0_DAYS or {_std?.sicknessWaitingPeriod}";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is not ShortTermDisability)
            {
                return false;
            }

            _std = (ShortTermDisability)value;
            string sicknessWaitingPeriod = _std.sicknessWaitingPeriod;
            string hospitalizationWaitingPeriod = _std.hospitalizationWaitingPeriod;
            string accidentWaitingPeriod = _std.accidentWaitingPeriod;

            return sicknessWaitingPeriod is EMPTY || hospitalizationWaitingPeriod is EMPTY || accidentWaitingPeriod is EMPTY
                || (sicknessWaitingPeriod is _7_DAYS && hospitalizationWaitingPeriod is _7_DAYS or _0_DAYS && accidentWaitingPeriod is _7_DAYS or _0_DAYS);
        }
    }
}

