using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public class PlanParameters : IPlanParameters
    {
        public string Name { get; set; }
        public List<Benefit> Benefits { get; set; }
    }
}
