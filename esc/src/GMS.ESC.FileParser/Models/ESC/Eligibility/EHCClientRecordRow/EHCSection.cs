namespace GMS.ESC.FileParser.Models.ESC.Eligibility.EHCClientRecordRow
{
    public class EHCSection
    {
        public string EHCProcessingMode { get; set; }
        public string CarrierEHCField { get; set; }
        public string EHCRecordEffectiveDate { get; set; }
        public string EHCRecordExpiryDate { get; set; }
        public string PlanNumber { get; set; }
        public string ClientBenefitOverrideCode { get; set; }
        public string PrivateCOBRuleNumber { get; set; }
        public string MaxDependantAge { get; set; }
        public string MaxStudentAge { get; set; }
        public string GeneralCode { get; set; }
        public string SuspendFlag { get; set; }
        public string CoverageCode { get; set; }
        public string COBStatusCode { get; set; }
        public string CostPlus { get; set; }
        public string EDIThresholdAmount { get; set; }
        public string Filler { get; set; }
    }
}
