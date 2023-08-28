using System;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public interface IClaim
    {
        public int ClaimantContractID { get; set; }
        public decimal TotalOwed { get; set; }
        public decimal TotalRefund { get; set; }
        public DateTime ServiceDate { get; set; }
        public int CoverageID { get; set; }
        public string CoverageDescription { get; set; }
        public int ClaimStateID { get; set; }
        public int PolicyID { get; set; }
    }
}
