using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class AllCoverageCodes
    {
        public static List<int> GetAllCoverageCodes(this IBenefit benefit) =>
            benefit.Coverages.SelectMany(coverage => coverage.Codes).ToList();
    }
}
