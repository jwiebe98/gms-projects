namespace GMS.ESC.FileParser.Models.ESC.Claims.Predetermination
{
    public class GeneralRecord
    {
        public string RecordIdentifier { get; set; }
        public string Predetermination { get; set; }
        public string LoadDate { set; get; }
        public string SettledDate { get; set; }
        public string ClientLanguageFlag { get; set; }
        public string ProviderNumber { set; get; }
        public string ProviderSurname { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderOfficeLocation { set; get; }
        public string ProviderProvince { get; set; }
        public string ProviderSpecialty { get; set; }
        public string ClientLastName { get; set; }
        public string ClientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientDateOfBirth { set; get; }
        public string SexOfPatient { get; set; }
        public string CarrierID { get; set; }
        public string GroupNumber { set; get; }
        public string SAS { get; set; }
        public string ClientID { get; set; }
        public string PatientCode { set; get; }
        public string PatientRelationshipCode { get; set; }
        public string OperatorID { get; set; }
        public string OperatorCode { get; set; }
        public string SubmissionMethod { get; set; }
        public string GeneralNotes { set; get; }
        public string GeneralNotesDate { set; get; }
        public string GeneralNotesUpdateID { set; get; }
        public string Status { get; set; }
        public string PrintOnLetter { get; set; }
        public string EndDate { set; get; }
        public string EDIAuthorizationInd { get; set; }
        public string ClientAddressLine1 { get; set; }
        public string ClientAddressLine2 { set; get; }
        public string ClientCity { get; set; }
        public string ClientProvince { get; set; }
        public string ClientCountry { get; set; }
        public string ClientPostalCode { get; set; }
        public string MailingIndicator { get; set; }
        public string AttachmentCode { get; set; }
        public string LetterCode { get; set; }
        public string Filler { set; get; }

    }

}
