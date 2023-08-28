using GMS.CIMS.BenefitsRemaining.Models;
using System;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class ReasonToSkipBenefit
    {
        public static bool GetReasonToSkipBenefit(this IBenefit benefit, IIndividualPlan plan, DateTime asOfDate)
        {
            var isTooOld = plan.DateOfBirth.AddYears(benefit.AgeMaximum) <= asOfDate;
            var isTooYoung = plan.DateOfBirth.AddYears(benefit.AgeMinimum) > asOfDate;
            var isWithinWaitingPeriod = plan.PlanStartDate.AddMonths(benefit.WaitingPeriod) > asOfDate;
            var benefitIsForPolicyHolderButIndividualIsNotPolicyHolder = benefit.Allocation.Equals(POLICY_HOLDER) && !plan.IsPolicyHolder;

            return isTooOld || isTooYoung || isWithinWaitingPeriod || benefitIsForPolicyHolderButIndividualIsNotPolicyHolder;
        }
    }
}
