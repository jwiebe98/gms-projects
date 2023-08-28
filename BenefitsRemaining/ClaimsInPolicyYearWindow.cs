using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class ClaimsInPolicyYearWindow
    {
        public static List<Claim> GetClaimsInPolicyYearWindow(this List<Claim> claims, IIndividualPlan plan, int frequency, DateTime asOfDate)
        {
            //var startYear = plan.GetStartYear(claims, frequency, asOfDate);
            var startYear = plan.GetPolicyYear(asOfDate).AddYears(-(frequency - 1));
            return claims.FindAll(c => c.ServiceDate >= startYear && c.ServiceDate <= asOfDate);
        }
    }
}
