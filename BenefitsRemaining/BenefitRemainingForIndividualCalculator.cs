using GMS.CIMS.BenefitsRemaining.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class BenefitRemainingForIndividualCalculator
    {
        public static BenefitsRemainingForIndividual CalculateBenefitsRemainingForIndividual(this IGrouping<int, IndividualPlan> plansForIndividual, List<Claim> claims, DateTime asOfDate)
        {
            var thisUsersClaims = claims.GetClaimsForIndividual(plansForIndividual.Key);

            List<BenefitRemaining> benefitsRemainingForIndividual = new();

            foreach (IIndividualPlan plan in plansForIndividual)
            {
                benefitsRemainingForIndividual = benefitsRemainingForIndividual.Concat(plan.CalculateBenefitsRemainingForPlan(thisUsersClaims, asOfDate)).ToList();
            }

            return new ()
            {
                ContractID = plansForIndividual.Key,
                FullName = plansForIndividual.First().FullName,
                BenefitsRemaining = benefitsRemainingForIndividual.OrderBy(br => br.DisplayPriority).ToList()
            };
        }
    }
}