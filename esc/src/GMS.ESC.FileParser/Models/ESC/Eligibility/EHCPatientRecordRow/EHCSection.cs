namespace GMS.ESC.FileParser.Models.ESC.Eligibility.EHCPatientRecordRow
{
    public class EHCSection
    {
        public string EHCProcessingMode { get; set; }
        public string EHCRecordEffectiveDate { get; set; }
        public string EHCRecordExpiryDate { get; set; }
        public string PlanNumber { get; set; }
        public string PatientBenefitOverrideCode { get; set; }
        public string COBStatus { get; set; }
        public string SuspendFlag { get; set; }
        public string LateEntrantIndicator { get; set; }
        public string CostPlus { get; set; }
        public string PrivateCOBRuleCode { get; set; }
        public string EDIThresholdAmount { get; set; }
        public string Filler { get; set; }
    }
}
