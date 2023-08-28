using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class BenefitRemainingCalculator
    {
        public static BenefitRemaining CalculateBenefitRemaining(this IBenefit benefit, List<Claim> claims, IIndividualPlan plan, DateTime asOfDate, string name, string displayType)
        {
            var totalClaimedInBenefitWindow = benefit.CalculateTotalClaimed(claims.GetClaimsInPolicyYearWindow(plan, benefit.Frequency, asOfDate));

            var totalClaimedInLifetime = benefit.CalculateTotalClaimed(claims.GetClaimsInPolicyYearWindow(plan, LIFETIME, asOfDate));

            var amountRemainingInCycle = benefit.Maximum < totalClaimedInBenefitWindow ? 0 : benefit.Maximum - totalClaimedInBenefitWindow;

            switch (displayType)
            {
                case SIMPLE:
                    return new()
                    {
                        Name = name,
                        AmountClaimedInCycle = totalClaimedInBenefitWindow,
                        Type = benefit.MaximumType,
                        DisplayPriority = benefit.DisplayPriority
                    };

                case COMPLETE:
                    return new()
                    {
                        Name = name,
                        BenefitMaximum = benefit.Maximum,
                        LifetimeMaximum = benefit.LifetimeMaximum,
                        AmountRemainingInCycle = amountRemainingInCycle,
                        AmountRemainingInLifeTime = benefit.LifetimeMaximum is null ? null : benefit.LifetimeMaximum < totalClaimedInLifetime ? 0 : benefit.LifetimeMaximum - totalClaimedInLifetime,
                        AmountClaimedInCycle = totalClaimedInBenefitWindow,
                        AmountClaimedInLifeTime = benefit.LifetimeMaximum is null ? null : totalClaimedInLifetime,
                        Type = benefit.MaximumType,
                        NextEligiblePolicyYear = GetNextEligibleDate(benefit, claims, plan, asOfDate, amountRemainingInCycle),
                        DisplayPriority = benefit.DisplayPriority
                    };

                default:
                    throw new Exception($"Unknown Display Type {displayType}");
            }
        }

        private static string GetNextEligibleDate(this IBenefit benefit, List<Claim> claims, IIndividualPlan plan, DateTime asOfDate, decimal amountRemainingInCycle)
        {
            if (benefit.Frequency <= 1)
            {
                return null;
            }

            else if (amountRemainingInCycle <= 0)
            {
                return plan.GetNextEligiblePolicyYear(claims, benefit.Frequency, asOfDate).ToString(DATE_DISPLAY_FORMAT);
            }

            else
            {
                return plan.GetPolicyYear(asOfDate).ToString(DATE_DISPLAY_FORMAT);
            }
        }
    }
}
