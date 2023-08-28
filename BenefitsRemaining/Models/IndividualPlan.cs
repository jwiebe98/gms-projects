using System;

namespace GMS.CIMS.BenefitsRemaining.Models
{
    public class IndividualPlan : IIndividualPlan
    {
        public int ContractID { get; set; }
        public int PolicyID { get; set; }
        public string PolicyDescription { get; set; }
        public int CompanyID { get; set; }
        public string FullName { get; set; }
        public bool IsPolicyHolder { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PlanParameters PlanParameters { get; set; }
        public DateTime AnniversaryDate { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime? PlanEndDate { get; set; }
    }
}
