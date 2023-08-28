using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.PatientRecordRow;

using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientRecordMappers.DentalSectionTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientRecordMappers.PharmacySectionTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientRecordMappers
{
    public static class PatientRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<PatientRecord> GetPatientRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PatientRecord());
            mapper.Property(x => x.RecordType, 2);
            mapper.Property(x => x.CarrierID, 2);
            mapper.Property(x => x.GroupNumber, 10);
            mapper.Property(x => x.SAS, 9);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.CurrentPatientCode, 3);
            mapper.Property(x => x.GeneralProcessingMode, 1);
            mapper.Property(x => x.RelationshipCode, 1);
            mapper.Property(x => x.FullPatientLastName, 30);
            mapper.Property(x => x.FullPatientFirstName, 30);
            mapper.Property(x => x.PatientMiddleInitial, 1);
            mapper.Property(x => x.PatientDateOfBirth, 8);
            mapper.Property(x => x.PatientSex, 1);
            mapper.Property(x => x.NewPatientCode, 3);
            mapper.Property(x => x.DentalEnrollmentdate, 8);
            mapper.Property(x => x.Filler, 58);
            mapper.ComplexProperty(x => x.PharmacySection, GetPharmacySectionTypeMapper(), 79);
            mapper.ComplexProperty(x => x.DentalSection, GetDentalSectionTypeMapper(), 96);
            return mapper;
        }
    }
}
