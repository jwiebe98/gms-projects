using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    [System.Serializable]
    public class BenefitRemaining : IBenefitRemaining
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
        public int DisplayPriority { get; set; } = 99;
        public List<BenefitRemaining> Practitioners { get; set; }
        public string FormattedBenefitMaximum { get { return BenefitMaximum.FormatForDisplay(Type); } }
        public string FormattedAmountRemainingInCycle { get { return AmountRemainingInCycle.FormatForDisplay(Type); } }
        public string FormattedAmountClaimedInCycle { get { return AmountClaimedInCycle.FormatForDisplay(Type); } }
        public string FormattedLifetimeMaximum { get { return LifetimeMaximum.FormatForDisplay(Type); } }
        public string FormattedAmountRemainingInLifeTime { get { return AmountRemainingInLifeTime.FormatForDisplay(Type); } }
        public string FormattedAmountClaimedInLifeTime { get { return AmountClaimedInLifeTime.FormatForDisplay(Type); } }
    }
}
