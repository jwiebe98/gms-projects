using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class PerPractitionerBenefitsRemaining
    {
        public static List<BenefitRemaining> GetPerPractitionerBenefitsRemaining(this IBenefit benefit, List<Claim> claims, IIndividualPlan plan, DateTime asOfDate)
        {
            List<BenefitRemaining> benefitsRemaining = benefit.GetBenefitsRemainingForEachCoverage(claims, plan, COMPLETE, asOfDate);

            if (benefit.Coverages.Count() == 1 && benefit.Coverages[0].Name.Equals(benefit.Name))
            {
                return benefitsRemaining;
            }

            return new()
            {
                new()
                {
                    Name = benefit.Name,
                    Practitioners = benefitsRemaining,
                    DisplayPriority = benefit.DisplayPriority
                }
            };
        }
    }
}
