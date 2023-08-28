using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class BenefitsRemainingForEachCoverage
    {
        public static List<BenefitRemaining> GetBenefitsRemainingForEachCoverage(this IBenefit benefit, List<Claim> claims, IIndividualPlan plan, string displayType, DateTime asOfDate)
        {
            List<BenefitRemaining> benefitsRemaining = new();

            foreach (ICoverage coverage in benefit.Coverages)
            {
                List<Claim> coveredClaims = claims.GetCoveredClaims(coverage.Codes);

                benefitsRemaining.Add(benefit.CalculateBenefitRemaining(coveredClaims, plan, asOfDate, coverage.Name, displayType));
            }

            return benefitsRemaining;
        }
    }
}
