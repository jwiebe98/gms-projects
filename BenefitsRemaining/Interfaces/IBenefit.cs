using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public interface IBenefit
    {
        public string Name { get; set; }
        public string AllocationType { get; set; }
        public string Allocation { get; set; }
        public string MaximumType { get; set; }
        public int Maximum { get; set; }
        public int Frequency { get; set; }
        public int? LifetimeMaximum { get; set; }
        public int AgeMaximum { get; set; }
        public int AgeMinimum { get; set; }
        public int WaitingPeriod { get; set; }
        public int DisplayPriority { get; set; }
        public List<Coverage> Coverages { get; set; }
    }
}
