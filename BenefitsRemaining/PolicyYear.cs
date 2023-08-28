using GMS.CIMS.BenefitsRemaining.Models;
using System;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class PolicyYear
    {
        public static DateTime GetPolicyYear(this IIndividualPlan plan, DateTime asOfDate)
        {
            DateTime policyStartDate;

            if (plan.AnniversaryDate.Month == 2 && plan.AnniversaryDate.Day == 29 && asOfDate.Year % 4 != 0)
            {
                policyStartDate = new DateTime(asOfDate.Year, plan.AnniversaryDate.Month, plan.AnniversaryDate.Day - 1);
            }
            else
            {
                policyStartDate = new DateTime(asOfDate.Year, plan.AnniversaryDate.Month, plan.AnniversaryDate.Day);
            }

            return policyStartDate > asOfDate ? policyStartDate.AddYears(-1) : policyStartDate;
        }
    }
}
