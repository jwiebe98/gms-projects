using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class ClaimsForIndividual
    {
        public static List<Claim> GetClaimsForIndividual(this List<Claim> claims, int contractID) =>
            claims.FindAll(c => c.ClaimantContractID == contractID);
    }
}
