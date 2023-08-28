using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Interfaces
{
    public interface IBenefitsRemainingForPolicy
    {
        public string PolicyPeriod { get; set; }
        public string PolicyNumber { get; set; }
        public List<BenefitsRemainingForIndividual> BenefitsRemainingForFamily { get; set; }
    }
}
