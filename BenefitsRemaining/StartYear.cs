using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class StartYear
    {
        public static DateTime GetStartYear(this IIndividualPlan plan, List<Claim> claims, int frequency, DateTime asOfDate)
        {
            if (frequency <= 0)
            {
                throw new Exception("Frequency cannot be 0 or less!");
            }

            var paidClaims = claims.GetPaidClaims();

            if (paidClaims.Count == 0)
            {
                return plan.GetPolicyYear(asOfDate);
            }

            var firstPaidClaimDate = paidClaims.Min(c => c.ServiceDate);
            var startYear = plan.GetPolicyYear(firstPaidClaimDate);

            while (startYear < asOfDate)
            {
                var paidClaimsInNextStartYear = paidClaims.FindAll(c => c.ServiceDate >= startYear && c.ServiceDate < startYear.AddYears(1));
                if (paidClaimsInNextStartYear.Count() == 0)
                {
                    if (startYear.AddYears(1) >= asOfDate) return startYear;
                    startYear = startYear.AddYears(1);
                }
                else
                {
                    if (startYear.AddYears(frequency) >= asOfDate) return startYear;
                    startYear = startYear.AddYears(frequency);
                }
            }

            return startYear;
        }
    }
}
