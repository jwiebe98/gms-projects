using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class EligiblePolicyIDs
    {
        public static IDictionary<int, List<int>> GetEligiblePolicyIDs(this List<IndividualPlan> plans, DateTime asOfDate)
        {
            Dictionary<int, List<int>> eligiblePolicyIDs = new();

            var pastPolicies = plans.FindAll(p => p.PlanEndDate < asOfDate).GroupBy(p => p.PolicyID).ToList();

            var activePolicies = plans.FindAll(p => p.PlanEndDate is null || p.PlanEndDate >= asOfDate).GroupBy(p => p.PolicyID);

            foreach (var activePolicy in activePolicies)
            {
                eligiblePolicyIDs.Add(activePolicy.Key, new());
                ComparePolicies(activePolicy, pastPolicies, eligiblePolicyIDs[activePolicy.Key]);
            }

            return eligiblePolicyIDs;
        }

        private static void ComparePolicies(IGrouping<int, IndividualPlan> currentPolicy, List<IGrouping<int, IndividualPlan>> pastPolicies, List<int> policyIds)
        {
            policyIds.Add(currentPolicy.Key);

            var previousPolicy = pastPolicies.Find(
                pastPolicy => pastPolicy.ToList().Find(
                    pastPlan => currentPolicy.ToList().Find(
                        currentPlan => currentPlan.PlanStartDate == pastPlan.PlanEndDate.Value.AddDays(1) && currentPlan.PolicyID != pastPlan.PolicyID && currentPlan.CompanyID != pastPlan.CompanyID
                        ) is not null
                    ) is not null
                );

            if (previousPolicy is not null)
            {
                ComparePolicies(previousPolicy, pastPolicies, policyIds);
            }
        }
    }
}
