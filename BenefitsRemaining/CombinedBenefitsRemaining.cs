using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class CombinedBenefitsRemaining
    {
        public static List<BenefitRemaining> GetCombinedBenefitsRemaining(this IBenefit benefit, List<Claim> claims, IIndividualPlan plan, DateTime asOfDate)
        {
            var completeBenefitRemaining = benefit.GetBenefitRemainingForAllCoverages(claims, plan, COMPLETE, asOfDate);

            if (benefit.Coverages.Count() > 1 && benefit.Coverages.GroupBy(c => c.MaximumType).Count() == 1)
            {
                return completeBenefitRemaining.CreateCombinedBenefitBreakdown(benefit, claims, plan, asOfDate);
            }

            if (benefit.Coverages.Count() > 1 && benefit.Coverages.GroupBy(c => c.MaximumType).Count() > 1)
            {
                return completeBenefitRemaining.CreateCombinedBenefitBreakdownForMultipleCoverageMaximumTypes(benefit, claims, plan, asOfDate);
            }

            if (!benefit.Coverages[0].Name.Equals(benefit.Name))
            {
                return benefit.UseCoverageNameForCombinedBenefit(completeBenefitRemaining);
            }

            return new()
            {
                completeBenefitRemaining
            };
        }
    }
}
