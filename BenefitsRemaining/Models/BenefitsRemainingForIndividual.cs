using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    [System.Serializable]
    public class BenefitsRemainingForIndividual : IBenefitsRemainingForIndividual
    {
        public int ContractID { get; set; }
        public string FullName { get; set; }
        public List<BenefitRemaining> BenefitsRemaining { get; set; } = new();
    }
}
