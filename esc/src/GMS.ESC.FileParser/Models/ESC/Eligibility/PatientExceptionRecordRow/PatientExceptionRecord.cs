namespace GMS.ESC.FileParser.Models.ESC.Eligibility.PatientExceptionRecordRow
{
    public class PatientExceptionRecord
    {
        public string RecordType { get; set; }
        public string CarrierID { get; set; }
        public string GroupNumber { get; set; }
        public string SAS { get; set; }
        public string ClientID { get; set; }
        public string PatientCode { get; set; }
        public PharmacySection PharmacySection { get; set; }
        public DentalSection DentalSection { get; set; }
    }
}
