namespace GMS.ESC.FileParser.Models.ESC.Eligibility.PatientRecordRow
{
    public class PatientRecord
    {
        public string RecordType { get; set; }
        public string CarrierID { get; set; }
        public string GroupNumber { get; set; }
        public string SAS { get; set; }
        public string ClientID { get; set; }
        public string CurrentPatientCode { get; set; }
        public string GeneralProcessingMode { get; set; }
        public string RelationshipCode { get; set; }
        public string FullPatientLastName { get; set; }
        public string FullPatientFirstName { get; set; }
        public string PatientMiddleInitial { get; set; }
        public string PatientDateOfBirth { get; set; }
        public string PatientSex { get; set; }
        public string NewPatientCode { get; set; }
        public string DentalEnrollmentdate { get; set; }
        public string Filler { get; set; }
        public PharmacySection PharmacySection { get; set; }
        public DentalSection DentalSection { get; set; }
    }
}
