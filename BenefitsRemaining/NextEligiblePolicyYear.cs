using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class NextEligiblePolicyYear
    {
        public static DateTime GetNextEligiblePolicyYear(this IIndividualPlan plan, List<Claim> claims, int frequency, DateTime asOfDate)
        {
            var paidClaimsInPolicyWindow = claims.GetPaidClaims().GetClaimsInPolicyYearWindow(plan, frequency, asOfDate);

            if (paidClaimsInPolicyWindow.Count == 0)
            {
                return plan.GetPolicyYear(asOfDate);
            }

            return plan.GetPolicyYear(paidClaimsInPolicyWindow.Min(c => c.ServiceDate)).AddYears(frequency);
        }
        //claims.GetClaimsInPolicyYearWindow(plan, frequency, asOfDate).Count == 0 ? plan.GetPolicyYear(asOfDate) : plan.GetStartYear(claims, frequency, asOfDate).AddYears(frequency);
    }
}
