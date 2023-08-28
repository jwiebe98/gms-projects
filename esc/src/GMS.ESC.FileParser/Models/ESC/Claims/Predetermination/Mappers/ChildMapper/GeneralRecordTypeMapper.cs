using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Claims.Predetermination;

namespace GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers
{
    public static class GeneralRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<GeneralRecord> GetGeneralRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new GeneralRecord());
            mapper.Property(x => x.RecordIdentifier, 1);
            mapper.Property(x => x.Predetermination, 14);
            mapper.Property(x => x.LoadDate, 8);
            mapper.Property(x => x.SettledDate, 8);
            mapper.Property(x => x.ClientLanguageFlag, 1);
            mapper.Property(x => x.ProviderNumber, 9);
            mapper.Property(x => x.ProviderSurname, 30);
            mapper.Property(x => x.ProviderFirstName, 30);
            mapper.Property(x => x.ProviderOfficeLocation, 4);
            mapper.Property(x => x.ProviderProvince, 2);
            mapper.Property(x => x.ProviderSpecialty, 2);
            mapper.Property(x => x.ClientLastName, 30);
            mapper.Property(x => x.ClientFirstName, 30);
            mapper.Property(x => x.PatientLastName, 30);
            mapper.Property(x => x.PatientFirstName, 30);
            mapper.Property(x => x.PatientDateOfBirth, 8);
            mapper.Property(x => x.SexOfPatient, 1);
            mapper.Property(x => x.CarrierID, 2);
            mapper.Property(x => x.GroupNumber, 10);
            mapper.Property(x => x.SAS, 9);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.PatientCode, 3);
            mapper.Property(x => x.PatientRelationshipCode, 1);
            mapper.Property(x => x.OperatorID, 8);
            mapper.Property(x => x.OperatorCode, 5);
            mapper.Property(x => x.SubmissionMethod, 1);
            mapper.Property(x => x.GeneralNotes, 480);
            mapper.Property(x => x.GeneralNotesDate, 8);
            mapper.Property(x => x.GeneralNotesUpdateID, 8);
            mapper.Property(x => x.Status, 1);
            mapper.Property(x => x.PrintOnLetter, 1);
            mapper.Property(x => x.EndDate, 8);
            mapper.Property(x => x.EDIAuthorizationInd, 1);
            mapper.Property(x => x.ClientAddressLine1, 35);
            mapper.Property(x => x.ClientAddressLine2, 35);
            mapper.Property(x => x.ClientCity, 35);
            mapper.Property(x => x.ClientCountry, 15);
            mapper.Property(x => x.ClientPostalCode, 9);
            mapper.Property(x => x.MailingIndicator, 1);
            mapper.Property(x => x.AttachmentCode, 1);
            mapper.Property(x => x.LetterCode, 3);
            mapper.Property(x => x.Filler, 312);
            return mapper;
        }
    }
}
