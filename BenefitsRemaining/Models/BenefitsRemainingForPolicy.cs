using GMS.CIMS.BenefitsRemaining.Interfaces;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    [System.Serializable]
    public class BenefitsRemainingForPolicy : IBenefitsRemainingForPolicy
    {
        public string PolicyPeriod { get; set; }
        public string PolicyNumber { get; set; }
        public List<BenefitsRemainingForIndividual> BenefitsRemainingForFamily { get; set; }
    }
}
