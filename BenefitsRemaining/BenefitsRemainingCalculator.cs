using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public class BenefitsRemainingCalculator : IBenefitsRemainingCalculator
    {
        private static DateTime asOfDate;

        public BenefitsRemainingCalculator(DateTime asOfDate)
        {
            BenefitsRemainingCalculator.asOfDate = DateTime.Parse(asOfDate.ToShortDateString());
        }

        public List<BenefitsRemainingForPolicy> CalculateBenefitsRemaining(List<Claim> claims, List<IndividualPlan> plans)
        {
            List<BenefitsRemainingForPolicy> benefitsRemainingForEachPolicy = new();

            Dictionary<int, List<Claim>> claimsMadeWithEligiblePolicies = claims.GetClaimsMadeWithEligiblePolicies(plans, asOfDate);

            var activePlans = plans.FindAll(p => p.PlanEndDate is null || p.PlanEndDate >= asOfDate);

            var plansGroupedByPolicyID = activePlans.GroupBy(pl => pl.PolicyID);

            foreach (var plansInPolicy in plansGroupedByPolicyID)
            {
                List<BenefitsRemainingForIndividual> benefitsRemainingForFamily = new();

                var activePlansGroupedByContractID = plansInPolicy.ToList().GroupBy(p => p.ContractID);

                foreach (var plansForIndividual in activePlansGroupedByContractID)
                {
                    benefitsRemainingForFamily.Add(plansForIndividual.CalculateBenefitsRemainingForIndividual(claimsMadeWithEligiblePolicies[plansInPolicy.Key], asOfDate));
                }

                benefitsRemainingForEachPolicy.Add(new()
                {
                    PolicyNumber = plansInPolicy.ToList().First().PolicyDescription,
                    PolicyPeriod = plansInPolicy.ToList().First().GetPolicyPeriod(asOfDate),
                    BenefitsRemainingForFamily = benefitsRemainingForFamily
                });
            }

            return benefitsRemainingForEachPolicy;
        }
    }
}
