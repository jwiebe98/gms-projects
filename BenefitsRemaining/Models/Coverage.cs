using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public class Coverage : ICoverage
    {
        public List<int> Codes { get; set; }
        public string Name { get; set; }
        public string MaximumType { get; set; }
        public int Maximum { get; set; }
    }
}
