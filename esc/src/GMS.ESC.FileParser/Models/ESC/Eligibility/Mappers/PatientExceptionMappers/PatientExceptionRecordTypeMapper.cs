using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.PatientExceptionRecordRow;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientExceptionRecordMappers.DentalSectionTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientExceptionRecordMappers.PharmacySectionTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientExceptionRecordMappers
{
    public static class PatientExceptionRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<PatientExceptionRecord> GetPatientExceptionRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PatientExceptionRecord());
            mapper.Property(x => x.RecordType, 2);
            mapper.Property(x => x.CarrierID, 2);
            mapper.Property(x => x.GroupNumber, 10);
            mapper.Property(x => x.SAS, 9);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.PatientCode, 3);

            mapper.ComplexProperty(x => x.PharmacySection, GetPharmacySectionTypeMapper(), 178);
            mapper.ComplexProperty(x => x.DentalSection, GetDentalSectionTypeMapper(), 138);
            return mapper;
        }
    }
}
