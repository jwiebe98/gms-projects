using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;
using System.Linq;
using static GMS.CIMS.BenefitsRemaining.Constants;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class TotalClaimedCalculator
    {
        public static decimal CalculateTotalClaimed(this IBenefit benefit, List<Claim> claims) =>
            CalculateTotalClaimedFromMaximumType(benefit.MaximumType, claims);

        public static decimal CalculateTotalClaimed(this ICoverage coverage, List<Claim> claims) =>
            CalculateTotalClaimedFromMaximumType(coverage.MaximumType, claims);

        private static decimal CalculateTotalClaimedFromMaximumType(string maximumType, List<Claim> claims)
        {
            return maximumType.Equals(DOLLAR) ?
                claims.Select(c => c.TotalOwed).Sum() -
                claims.Select(c => c.TotalRefund).Sum() :
                maximumType.Equals(COUNT) ?
                claims.GetPaidClaims().Count() -
                claims.FindAll(c => c.TotalRefund != 0).Count() : 0;
        }
    }
}
