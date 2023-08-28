using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class CoverageNameForCombinedBenefit
    {
        public static List<BenefitRemaining> UseCoverageNameForCombinedBenefit(this IBenefit benefit, BenefitRemaining benefitRemaining)
        {
            benefitRemaining.Name = benefit.Coverages[0].Name;

            return new()
            {
                new()
                {
                    Name = benefit.Name,
                    Practitioners = new()
                    {
                        benefitRemaining
                    },
                    DisplayPriority = benefit.DisplayPriority
                }
            };
        }

    }
}
