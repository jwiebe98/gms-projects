using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public class Benefit : IBenefit
    {
        public string Name { get; set; }
        public string AllocationType { get; set; }
        public string Allocation { get; set; }
        public string MaximumType { get; set; }
        public int Maximum { get; set; }
        public int Frequency { get; set; }
        public int? LifetimeMaximum { get; set; }
        public int AgeMaximum { get; set; } = 999;
        public int AgeMinimum { get; set; } = 0;
        public int WaitingPeriod { get; set; } = 0;
        public int DisplayPriority { get; set; } = 99;
        public List<Coverage> Coverages { get; set; }
    }
}
