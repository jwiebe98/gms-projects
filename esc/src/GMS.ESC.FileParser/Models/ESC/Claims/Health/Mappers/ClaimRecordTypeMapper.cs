using FlatFiles.TypeMapping;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.ClaimsTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers
{
    public static class ClaimRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<ClaimRecord> GetClaimRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ClaimRecord());

            mapper.Property(x => x.RecordIdentifier, 1).Trim(false);
            mapper.Property(x => x.TransReferenceNumber, 14).Trim(false);
            mapper.Property(x => x.TransCrossReferenceNumber, 14).Trim(false);
            mapper.Property(x => x.OfficeSequenceNumber, 6).Trim(false);
            mapper.Property(x => x.DateClaimReceived, 8).Trim(false);
            mapper.Property(x => x.DateProcessedAdjudicated, 8).Trim(false);
            mapper.Property(x => x.ClaimStatus, 1).Trim(false);
            mapper.Property(x => x.ClientLanguageFlag, 1).Trim(false);
            mapper.Property(x => x.ProviderNumber, 9).Trim(false);
            mapper.Property(x => x.ProviderSurname, 30).Trim(false);
            mapper.Property(x => x.ProviderFirstName, 30).Trim(false);
            mapper.Property(x => x.Filler1, 20).Trim(false);
            mapper.Property(x => x.ProviderOfficeLocationNumber, 4).Trim(false);
            mapper.Property(x => x.ProviderProvince, 2).Trim(false);
            mapper.Property(x => x.ProviderSoftwareVendorCode, 3).Trim(false);
            mapper.Property(x => x.ProviderSpecialty, 2).Trim(false);
            mapper.Property(x => x.Filler2, 22).Trim(false);
            mapper.Property(x => x.PendDate, 8).Trim(false);
            mapper.Property(x => x.PendingReason, 2).Trim(false);
            mapper.Property(x => x.ClientLastName, 30).Trim(false);
            mapper.Property(x => x.ClientFirstName, 30).Trim(false);
            mapper.Property(x => x.PatientLastName, 30).Trim(false);
            mapper.Property(x => x.PatientFirstName, 30).Trim(false);
            mapper.Property(x => x.PatientDateOfBirth, 8).Trim(false);
            mapper.Property(x => x.SexOfPatient, 1).Trim(false);
            mapper.Property(x => x.CarrierID, 2).Trim(false);
            mapper.Property(x => x.GSAS, 19).Trim(false);
            mapper.Property(x => x.ClientID, 15).Trim(false);
            mapper.Property(x => x.PatientCode, 3).Trim(false);
            mapper.Property(x => x.PatientRelationshipCode, 1).Trim(false);
            mapper.Property(x => x.OperatorID, 8).Trim(false);
            mapper.Property(x => x.OperatorCode, 5).Trim(false);
            mapper.Property(x => x.ClaimSubmissionMethod, 1).Trim(false);
            mapper.Property(x => x.PayeeCode, 1).Trim(false);
            mapper.Property(x => x.PaymentMethod, 1).Trim(false);
            mapper.Property(x => x.TotalAmountClaimed, 7).Trim(false);
            mapper.Property(x => x.AdjustmentAmount, 6).Trim(false);
            mapper.Property(x => x.AdjustmentReason, 2).Trim(false);
            mapper.Property(x => x.TotalAmountPaid, 7).Trim(false);
            mapper.Property(x => x.ErrorCodes, 12).Trim(false);
            mapper.Property(x => x.ESIMessages1, 12).Trim(false);
            mapper.Property(x => x.MaterialForwarded, 1).Trim(false);
            mapper.Property(x => x.SchoolName, 25).Trim(false);
            mapper.Property(x => x.SecondaryCoverage, 1).Trim(false);
            mapper.Property(x => x.AccidentDate, 8).Trim(false);
            mapper.Property(x => x.PredeterminationNumber, 14).Trim(false);
            mapper.Property(x => x.LineOfBusinessCode, 3).Trim(false);
            mapper.Property(x => x.AttachmentCode, 1).Trim(false);
            mapper.Property(x => x.LetterCode, 3).Trim(false);
            mapper.Property(x => x.GeneralCodeGSAS, 10).Trim(false);
            mapper.Property(x => x.GeneralCodeClient, 1).Trim(false);
            mapper.Property(x => x.DistributionCode, 1).Trim(false);
            mapper.Property(x => x.FreeFormMessage, 480).Trim(false);
            mapper.Property(x => x.NumberOfServiceCodesOnThisRecord, 2).Trim(false);
            mapper.Property(x => x.ClientAddressLine1, 35).Trim(false);
            mapper.Property(x => x.ClientAddressLine2, 35).Trim(false);
            mapper.Property(x => x.ClientCity, 35).Trim(false);
            mapper.Property(x => x.ClientProvince, 2).Trim(false);
            mapper.Property(x => x.ClientCountry, 15);
            mapper.Property(x => x.ClientPostalCode, 9).Trim(false);
            mapper.Property(x => x.OverrideIndicator, 1).Trim(false);
            mapper.Property(x => x.SupressEob, 1).Trim(false);
            mapper.Property(x => x.Filler3, 97).Trim(false);

            mapper.ComplexProperty(x => x.Claims, GetClaimsTypeMapper(), 3164);

            mapper.Property(x => x.Filler7, 200).Trim(false);
            return mapper;
        }
    }
}
