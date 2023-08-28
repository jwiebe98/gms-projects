using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public interface IBenefitRemaining
    {
        public string Name { get; set; }
        public decimal? BenefitMaximum { get; set; }
        public decimal? AmountRemainingInCycle { get; set; }
        public decimal? AmountClaimedInCycle { get; set; }
        public decimal? LifetimeMaximum { get; set; }
        public decimal? AmountRemainingInLifeTime { get; set; }
        public decimal? AmountClaimedInLifeTime { get; set; }
        public string NextEligiblePolicyYear { get; set; }
        public string Type { get; set; }
        public int DisplayPriority { get; set; }
        public List<BenefitRemaining> Practitioners { get; set; }
        public string FormattedBenefitMaximum { get; }
        public string FormattedAmountRemainingInCycle { get; }
        public string FormattedAmountClaimedInCycle { get; }
        public string FormattedLifetimeMaximum { get; }
        public string FormattedAmountRemainingInLifeTime { get; }
        public string FormattedAmountClaimedInLifeTime { get; }
    }
}
