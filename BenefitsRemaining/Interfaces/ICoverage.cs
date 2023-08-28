using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public interface ICoverage
    {
        public List<int> Codes { get; set; }
        public string Name { get; set; }
        public string MaximumType { get; set; }
        public int Maximum { get; set; }
    }
}
