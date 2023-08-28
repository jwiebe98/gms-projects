using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class ClaimsMadeWithEligiblePolicies
    {
        public static Dictionary<int, List<Claim>> GetClaimsMadeWithEligiblePolicies(this List<Claim> claims, List<IndividualPlan> plans, DateTime asOfDate)
        {
            var eligiblePolicies = plans.GetEligiblePolicyIDs(asOfDate);

            Dictionary<int, List<Claim>> eligibleClaims = new();

            foreach (var eligiblePolicy in eligiblePolicies)
            {
                eligibleClaims.Add(eligiblePolicy.Key, claims.FindAll(c => eligiblePolicy.Value.Contains(c.PolicyID)));
            }

            return eligibleClaims;
        }
    }
}
