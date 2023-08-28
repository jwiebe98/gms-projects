using System.Collections.Generic;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental
{
    public class ClaimRecord
    {
        public string RecordIdentifier { get; set; }
        public string TransReferenceNumber { get; set; }
        public string TransCrossReferenceNumber { get; set; }
        public string OfficeSequenceNumber { get; set; }
        public string DateClaimReceived { get; set; }
        public string DateProcessedAdjudicated { get; set; }
        public string ClaimStatus { get; set; }
        public string ClientLanguageFlag { get; set; }
        public string ProviderNumber { get; set; }
        public string ProviderSurname { get; set; }
        public string ProviderFirstName { get; set; }
        public string Filler1 { get; set; }
        public string ProviderOfficeLocationNumber { get; set; }
        public string ProviderProvince { get; set; }
        public string ProviderSoftwareVendorCode { get; set; }
        public string ProviderSpecialty { get; set; }
        public string Filler2 { get; set; }
        public string PendDate { get; set; }
        public string PendingReason { get; set; }
        public string ClientLastName { get; set; }
        public string ClientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientDateOfBirth { get; set; }
        public string SexOfPatient { get; set; }
        public string CarrierID { get; set; }
        public string GSAS { get; set; }
        public string ClientID { get; set; }
        public string PatientCode { get; set; }
        public string PatientRelationshipCode { get; set; }
        public string OperatorID { get; set; }
        public string OperatorCode { get; set; }
        public string ClaimSubmissionMethod { get; set; }
        public string PayeeCode { get; set; }
        public string PaymentMethod { get; set; }
        public string TotalAmountClaimed { get; set; }
        public string AdjustmentAmount { get; set; }
        public string AdjustmentReason { get; set; }
        public string TotalAmountPaid { get; set; }
        public string ErrorCodes { get; set; }
        public string ESIMessages1 { get; set; }
        public string MaterialForwarded { get; set; }
        public string SchoolName { get; set; }
        public string SecondaryCoverage { get; set; }
        public string AccidentDate { get; set; }
        public string PredeterminationNumber { get; set; }
        public string LineOfBusinessCode { get; set; }
        public string AttachmentCode { get; set; }
        public string LetterCode { get; set; }
        public string GeneralCodeGSAS { get; set; }
        public string GeneralCodeClient { get; set; }
        public string DistributionCode { get; set; }
        public string FreeFormMessage { get; set; }
        public string NumberOfServiceCodesOnThisRecord { get; set; }
        public string ClientAddressLine1 { get; set; }
        public string ClientAddressLine2 { get; set; }
        public string ClientCity { get; set; }
        public string ClientProvince { get; set; }
        public string ClientCountry { get; set; }
        public string ClientPostalCode { get; set; }
        public string OverrideIndicator { get; set; }
        public string SupressEob { get; set; }
        public string Filler3 { get; set; }
        public List<Claim> Claims { get; set; }
        public string Filler7 { get; set; }
    }
}
