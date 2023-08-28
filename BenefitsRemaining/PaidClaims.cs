using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class PaidClaims
    {
        public static List<Claim> GetPaidClaims(this List<Claim> claims) =>
            claims.FindAll(c => c.ClaimStateID == PAID);
    }
}
