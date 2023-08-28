using GMS.CIMS.BenefitsRemaining.Models;
using System;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class PolicyPeriod
    {
        public static string GetPolicyPeriod(this IIndividualPlan individualPlan, DateTime asOfDate)
        {
            var startOfPolicyYear = individualPlan.GetPolicyYear(asOfDate);
            var endOfPolicyYear = startOfPolicyYear.AddYears(1).AddDays(-1);

            return startOfPolicyYear.ToString(POLICY_YEAR_PERIOD_DATE_FORMAT) + " - " + endOfPolicyYear.ToString(POLICY_YEAR_PERIOD_DATE_FORMAT);
        }
    }
}
