using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.EHCPatientRecordRow;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCPatientRecordMappers.EHCSectionTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCPatientRecordMappers
{
    public static class EHCPatientRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<EHCPatientRecord> GetEHCPatientRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new EHCPatientRecord());
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
            mapper.Property(x => x.EHCEnrollmentDate, 8);
            mapper.Property(x => x.Filler, 58);

            mapper.ComplexProperty(x => x.EHCSection, GetEHCSectionTypeMapper(), 175);
            return mapper;
        }
    }
}
