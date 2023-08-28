namespace GMS.ESC.FileParser.Models.ESC.Claims.Health
{
    public class Claim
    {
        public string SubmittedLineNumber { get; set; }
        public string PaidLineNumber { get; set; }
        public string ServiceStatus { get; set; }
        public string AdjudicationRuleNumberApplied { get; set; }
        public string FrequencyRuleNumberApplied { get; set; }
        public string DateOfService { get; set; }
        public string ServiceCode { get; set; }
        public string Filler4 { get; set; }
        public string ServiceNameEnglish { get; set; }
        public string ServiceNameFrench { get; set; }
        public string ToothCode { get; set; }
        public string ToothSurface { get; set; }
        public string ProfessionalFeeClaimed { get; set; }
        public string PreviouslyPaidAmount { get; set; }
        public string ProfessionalFeeEligibleAmount { get; set; }
        public string DeductibleAmountProfessionalFee { get; set; }
        public string ProfessionalFeeBenefitAmount { get; set; }
        public string LabProcedureCode { get; set; }
        public string LabFeeClaimed { get; set; }
        public string EligibleAmountLab { get; set; }
        public string LabDeductibleAmount { get; set; }
        public string LabBenefitAmount { get; set; }
        public string ExpenseProcedureCode { get; set; }
        public string ExpenseClaimed { get; set; }
        public string ExpenseEligibleAmount { get; set; }
        public string ExpenseDeductibleAmount { get; set; }
        public string ExpenseBenefitAmount { get; set; }
        public string ErrorCode { get; set; }
        public string ESIMessages2 { get; set; }
        public string TotalFeesClaimed { get; set; }
        public string CoinsuranceAmount { get; set; }
        public string CoinsurancePercentage { get; set; }
        public string TotalFeesPaid { get; set; }
        public string PaidServiceCode1 { get; set; }
        public string PaidServiceCode2 { get; set; }
        public string Filler5 { get; set; }
        public string PlanNumber { get; set; }
        public string BenefitCode { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryLabelEnglish { get; set; }
        public string CategoryLabelFrench { get; set; }
        public string CoverageCodeFromEligibility { get; set; }
        public string CarrierEHCField { get; set; }
        public string ServiceCodeSource { get; set; }
        public string MaximumCutbackAmount { get; set; }
        public string RuleCutbackAmount { get; set; }
        public string FeeGuideAmount { get; set; }
        public string Filler6 { get; set; }
    }
}
