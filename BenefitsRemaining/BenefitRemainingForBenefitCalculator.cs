using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class BenefitRemainingForBenefitCalculator
    {
        public static List<BenefitRemaining> CalculateBenefitRemainingForBenefit(this IBenefit benefit, List<Claim> claims, IIndividualPlan plan, DateTime asOfDate)
        {
            switch (benefit.AllocationType)
            {
                case COMBINED:
                    return benefit.GetCombinedBenefitsRemaining(claims, plan, asOfDate);

                case PER_PRACTITIONER:
                    return benefit.GetPerPractitionerBenefitsRemaining(claims, plan, asOfDate);

                default:
                    throw new Exception($"Unknown Practitioner {benefit.AllocationType}");
            }
        }
    }
}
