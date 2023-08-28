﻿namespace GMS.ESC.FileParser.Models.ESC.Eligibility.EHCClientRecordRow
{
    public class EHCClientRecord
    {
        public string RecordType { get; set; }
        public string CarrierID { get; set; }
        public string GroupNumber { get; set; }
        public string SAS { get; set; }
        public string ClientID { get; set; }
        public string GeneralProcessingMode { get; set; }
        public string AlternateIdentification { get; set; }
        public string ClientLanguageFlagCode { get; set; }
        public string ClientProvinceCode { get; set; }
        public string EFTAccountNumber { get; set; }
        public string EFTRouteCode { get; set; }
        public string EFTEffectiveDate { get; set; }
        public string EFTTerminationDate { get; set; }
        public string LookupOverrideCode { get; set; }
        public string EmploymentEnrollmentDate { get; set; }
        public string Filler { get; set; }
        public EHCSection EHCSection { get; set; }
    }
}
