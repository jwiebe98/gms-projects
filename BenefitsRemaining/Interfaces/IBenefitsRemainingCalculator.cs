using GMS.CIMS.BenefitsRemaining.Interfaces;
using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public interface IBenefitsRemainingCalculator
    {
        public List<BenefitsRemainingForPolicy> CalculateBenefitsRemaining(List<Claim> claims, List<IndividualPlan> plans);
    }
}
