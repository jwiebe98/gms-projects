using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class CombinedBenefitBreakdown
    {
        public static List<BenefitRemaining> CreateCombinedBenefitBreakdown(this BenefitRemaining benefitRemaining, IBenefit benefit, List<Claim> claims, IIndividualPlan plan, DateTime asOfDate)
        {
            List<BenefitRemaining> combinedBenefitBreakdown = benefit.GetBenefitsRemainingForEachCoverage(claims, plan, SIMPLE, asOfDate);

            benefitRemaining.Practitioners = combinedBenefitBreakdown;

            return new()
            {
                benefitRemaining
            };
        }
    }
}
