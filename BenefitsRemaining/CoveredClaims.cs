using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class CoveredClaims
    {
        public static List<Claim> GetCoveredClaims(this List<Claim> claims, List<int> coverageCodes) =>
            claims.FindAll(c => coverageCodes.Contains(c.CoverageID));
    }
}
