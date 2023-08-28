using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class BenefitsRemainingForAllCoverages
    {
        public static BenefitRemaining GetBenefitRemainingForAllCoverages(this IBenefit benefit, List<Claim> claims, IIndividualPlan plan, string displayType, DateTime asOfDate)
        {
            var allCoverageCodes = benefit.GetAllCoverageCodes();

            List<Claim> allCoveredClaims = claims.GetCoveredClaims(allCoverageCodes);

            return benefit.CalculateBenefitRemaining(allCoveredClaims, plan, asOfDate, benefit.Name, displayType);
        }
    }
}
