using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class CombinedBenefitBreakdownForMultipleCoverageMaximumTypes
    {
        public static List<BenefitRemaining> CreateCombinedBenefitBreakdownForMultipleCoverageMaximumTypes(this BenefitRemaining benefitRemaining, IBenefit benefit, List<Claim> claims, IIndividualPlan plan, DateTime asOfDate)
        {
            benefitRemaining.NextEligiblePolicyYear = null;

            var allCoverageCodes = benefit.GetAllCoverageCodes();

            var claimsForBenefit = claims.GetCoveredClaims(allCoverageCodes);

            List<BenefitRemaining> combinedBenefitBreakdown = new();

            foreach (ICoverage coverage in benefit.Coverages)
            {
                if (!coverage.MaximumType.Equals(DOLLAR) && !coverage.MaximumType.Equals(COUNT))
                {
                    throw new Exception($"Unknown Maximum Type: {coverage.MaximumType}");
                }

                List<Claim> claimsForCoverage = claims.GetCoveredClaims(coverage.Codes);

                var totalClaimedInBenefitWindow = coverage.CalculateTotalClaimed(claimsForCoverage.GetClaimsInPolicyYearWindow(plan, benefit.Frequency, asOfDate));

                BenefitRemaining combinedBenefit = new()
                {
                    Name = coverage.Name,
                    AmountClaimedInCycle = totalClaimedInBenefitWindow,
                    Type = coverage.MaximumType,
                    DisplayPriority = benefit.DisplayPriority,
                };

                switch (coverage.MaximumType)
                {
                    case COUNT:
                        combinedBenefit.BenefitMaximum = coverage.Maximum;
                        combinedBenefit.AmountRemainingInCycle = coverage.Maximum < totalClaimedInBenefitWindow || benefitRemaining.AmountRemainingInCycle <= 0 ? 0 : coverage.Maximum - totalClaimedInBenefitWindow;
                        combinedBenefit.NextEligiblePolicyYear =
                            benefit.Frequency == 1 ?
                            null :
                            benefitRemaining.AmountRemainingInCycle == 0 ?
                            plan.GetNextEligiblePolicyYear(claimsForBenefit, benefit.Frequency, asOfDate).ToString(DATE_DISPLAY_FORMAT) :
                            totalClaimedInBenefitWindow >= coverage.Maximum ?
                            plan.GetNextEligiblePolicyYear(claimsForCoverage, benefit.Frequency, asOfDate).ToString(DATE_DISPLAY_FORMAT) :
                            plan.GetPolicyYear(asOfDate).ToString(DATE_DISPLAY_FORMAT);
                        break;

                    case DOLLAR:
                        combinedBenefit.NextEligiblePolicyYear =
                            benefit.Frequency == 1 ?
                            null :
                            benefitRemaining.AmountRemainingInCycle == 0 ?
                            plan.GetNextEligiblePolicyYear(claimsForBenefit, benefit.Frequency, asOfDate).ToString(DATE_DISPLAY_FORMAT) :
                            plan.GetPolicyYear(asOfDate).ToString(DATE_DISPLAY_FORMAT);
                        break;
                }

                combinedBenefitBreakdown.Add(combinedBenefit);
            }

            benefitRemaining.Practitioners = combinedBenefitBreakdown;

            return new()
            {
                benefitRemaining
            };
        }
    }
}
