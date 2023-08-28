using System;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public class Claim : IClaim
    {
        public int ClaimID { get; set; }
        public int ClaimantContractID { get; set; }
        public decimal TotalOwed { get; set; }
        public decimal TotalRefund { get; set; } = 0;
        public DateTime ServiceDate { get; set; }
        public int CoverageID { get; set; }
        public string CoverageDescription { get; set; }
        public int ClaimStateID { get; set; }
        public int PolicyID { get; set; }
    }
}
