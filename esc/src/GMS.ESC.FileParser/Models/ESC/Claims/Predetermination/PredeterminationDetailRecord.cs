namespace GMS.ESC.FileParser.Models.ESC.Claims.Predetermination
{
    public class PredeterminationDetailRecord
    {
        public string RecordIdentifier { get; set; }
        public string PDLineNumber { set; get; }
        public string DateProcessed { set; get; }
        public string AdjudicationCode { get; set; }
        public string AdjudicationRuleNumberApplied { get; set; }
        public string FrequencyRuleNumberApplied { set; get; }
        public string ServiceCode { get; set; }
        public string ServiceNameEnglish { get; set; }
        public string ServiceNameFrench { set; get; }
        public string ToothCode { get; set; }
        public string ToothSurface { get; set; }
        public string ProfessionalFeeClaimed { set; get; }
        public string ProfessionalFeeEligibleAmount { get; set; }
        public string DeductibleAmountProfessionalFee { get; set; }
        public string ProfessionalFeeBenefitAmount { get; set; }
        public string LabProcedureCode { get; set; }
        public string LabFeeClaimed { set; get; }
        public string EligibleAmountLab { set; get; }
        public string LabDeductibleAmount { set; get; }
        public string LabBenefitAmount { get; set; }
        public string ExpenseProcedureCode { get; set; }
        public string ExpenseClaimed { set; get; }
        public string ExpenseEligibleAmount { get; set; }
        public string ExpenseDeductibleAmount { get; set; }
        public string ExpenseBenefitAmount { set; get; }
        public string ESIMessages { get; set; }
        public string TotalFeesClaimed { get; set; }
        public string CoinsuranceAmount { set; get; }
        public string CoinsurancePercentage { get; set; }
        public string TotalFeesPaid { get; set; }
        public string PaidServiceCode1 { get; set; }
        public string PaidServiceCode2 { get; set; }
        public string Filler1 { set; get; }
        public string PlanNumber { set; get; }
        public string BenefitCode { set; get; }
        public string CategoryCode { get; set; }
        public string CategorylabelEng1 { get; set; }
        public string CategorylabelEng2 { set; get; }
        public string CoverageCodeFromEligibility { get; set; }
        public string CarrierDentalField { get; set; }
        public string ServiceCodeSource { set; get; }
        public string MaximumCutbackAmount { get; set; }
        public string RuleCutbackAmount { get; set; }
        public string FeeGuideAmount { set; get; }
        public string AlternateServiceCode { get; set; }
        public string EquivalentServiceCode { get; set; }
        public string Filler2 { get; set; }
    }
}
