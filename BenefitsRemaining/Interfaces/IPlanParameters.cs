using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public interface IPlanParameters
    {
        public string Name { get; set; }
        public List<Benefit> Benefits { get; set; }
    }
}
