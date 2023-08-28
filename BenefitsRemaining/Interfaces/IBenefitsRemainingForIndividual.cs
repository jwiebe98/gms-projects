using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public interface IBenefitsRemainingForIndividual
    {
        public int ContractID { get; set; }
        public string FullName { get; set; }
        public List<BenefitRemaining> BenefitsRemaining { get; set; }
    }
}
