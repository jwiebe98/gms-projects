using FlatFiles.TypeMapping;
using static GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers.ClaimsTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers
{
    public static class ClaimRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<ClaimRecord> GetClaimRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ClaimRecord());

            mapper.Property(x => x.RecordIdentifier, 1);
            mapper.Property(x => x.TransReferenceNumber, 14);
            mapper.Property(x => x.TransCrossReferenceNumber, 14);
            mapper.Property(x => x.OfficeSequenceNumber, 6);
            mapper.Property(x => x.DateClaimReceived, 8);
            mapper.Property(x => x.DateProcessedAdjudicated, 8);
            mapper.Property(x => x.ClaimStatus, 1);
            mapper.Property(x => x.ClientLanguageFlag, 1);
            mapper.Property(x => x.ProviderNumber, 9);
            mapper.Property(x => x.ProviderSurname, 30);
            mapper.Property(x => x.ProviderFirstName, 30);
            mapper.Property(x => x.Filler1, 20);
            mapper.Property(x => x.ProviderOfficeLocationNumber, 4);
            mapper.Property(x => x.ProviderProvince, 2);
            mapper.Property(x => x.ProviderSoftwareVendorCode, 3);
            mapper.Property(x => x.ProviderSpecialty, 2);
            mapper.Property(x => x.Filler2, 22);
            mapper.Property(x => x.PendDate, 8);
            mapper.Property(x => x.PendingReason, 2);
            mapper.Property(x => x.ClientLastName, 30);
            mapper.Property(x => x.ClientFirstName, 30);
            mapper.Property(x => x.PatientLastName, 30);
            mapper.Property(x => x.PatientFirstName, 30);
            mapper.Property(x => x.PatientDateOfBirth, 8);
            mapper.Property(x => x.SexOfPatient, 1);
            mapper.Property(x => x.CarrierID, 2);
            mapper.Property(x => x.GSAS, 19);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.PatientCode, 3);
            mapper.Property(x => x.PatientRelationshipCode, 1);
            mapper.Property(x => x.OperatorID, 8);
            mapper.Property(x => x.OperatorCode, 5);
            mapper.Property(x => x.ClaimSubmissionMethod, 1);
            mapper.Property(x => x.PayeeCode, 1);
            mapper.Property(x => x.PaymentMethod, 1);
            mapper.Property(x => x.TotalAmountClaimed, 7);
            mapper.Property(x => x.AdjustmentAmount, 6);
            mapper.Property(x => x.AdjustmentReason, 2);
            mapper.Property(x => x.TotalAmountPaid, 7);
            mapper.Property(x => x.ErrorCodes, 12);
            mapper.Property(x => x.ESIMessages1, 12);
            mapper.Property(x => x.MaterialForwarded, 1);
            mapper.Property(x => x.SchoolName, 25);
            mapper.Property(x => x.SecondaryCoverage, 1);
            mapper.Property(x => x.AccidentDate, 8);
            mapper.Property(x => x.PredeterminationNumber, 14);
            mapper.Property(x => x.LineOfBusinessCode, 3);
            mapper.Property(x => x.AttachmentCode, 1);
            mapper.Property(x => x.LetterCode, 3);
            mapper.Property(x => x.GeneralCodeGSAS, 10);
            mapper.Property(x => x.GeneralCodeClient, 1);
            mapper.Property(x => x.DistributionCode, 1);
            mapper.Property(x => x.FreeFormMessage, 480);
            mapper.Property(x => x.NumberOfServiceCodesOnThisRecord, 2);
            mapper.Property(x => x.ClientAddressLine1, 35);
            mapper.Property(x => x.ClientAddressLine2, 35);
            mapper.Property(x => x.ClientCity, 35);
            mapper.Property(x => x.ClientProvince, 2);
            mapper.Property(x => x.ClientCountry, 15);
            mapper.Property(x => x.ClientPostalCode, 9);
            mapper.Property(x => x.OverrideIndicator, 1);
            mapper.Property(x => x.SupressEob, 1);
            mapper.Property(x => x.Filler3, 97);

            mapper.ComplexProperty(x => x.Claims, GetClaimsTypeMapper(), 3164);

            mapper.Property(x => x.Filler7, 200);
            return mapper;
        }
    }
}
