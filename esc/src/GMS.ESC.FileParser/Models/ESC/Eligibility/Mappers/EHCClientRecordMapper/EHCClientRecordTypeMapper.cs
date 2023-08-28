using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.EHCClientRecordRow;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCClientRecordMappers.EHCSectionTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCClientRecordMappers
{
    public static class EHCClientRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<EHCClientRecord> GetEHCClientRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new EHCClientRecord());
            mapper.Property(x => x.RecordType, 2);
            mapper.Property(x => x.CarrierID, 2);
            mapper.Property(x => x.GroupNumber, 10);
            mapper.Property(x => x.SAS, 9);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.GeneralProcessingMode, 1);
            mapper.Property(x => x.AlternateIdentification, 16);
            mapper.Property(x => x.ClientLanguageFlagCode, 1);
            mapper.Property(x => x.ClientProvinceCode, 2);
            mapper.Property(x => x.EFTAccountNumber, 12);
            mapper.Property(x => x.EFTRouteCode, 9);
            mapper.Property(x => x.EFTEffectiveDate, 8);
            mapper.Property(x => x.EFTTerminationDate, 8);
            mapper.Property(x => x.LookupOverrideCode, 1);
            mapper.Property(x => x.EmploymentEnrollmentDate, 8);
            mapper.Property(x => x.Filler, 50);

            mapper.ComplexProperty(x => x.EHCSection, GetEHCSectionTypeMapper(), 203);
            return mapper;
        }
    }
}
