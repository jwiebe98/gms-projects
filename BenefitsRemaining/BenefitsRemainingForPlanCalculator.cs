using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class BenefitsRemainingForPlanCalculator
    {
        public static List<BenefitRemaining> CalculateBenefitsRemainingForPlan(this IIndividualPlan plan, List<Claim> claims, DateTime asOfDate)
        {
            List<BenefitRemaining> benefitsRemaining = new();

            foreach (IBenefit benefit in plan.PlanParameters.Benefits)
            {
                if (!benefit.GetReasonToSkipBenefit(plan, asOfDate))
                {
                    benefitsRemaining = benefitsRemaining.Concat(benefit.CalculateBenefitRemainingForBenefit(claims, plan, asOfDate)).ToList();
                }
            }

            return benefitsRemaining.GetGroupedBenefitsRemainingByBenefitName();
        }
    }
}
