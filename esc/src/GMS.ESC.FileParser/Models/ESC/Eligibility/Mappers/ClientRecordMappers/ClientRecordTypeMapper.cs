using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.ClientRecordRow;

using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.ClientRecordMappers.DentalSectionTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.ClientRecordMappers.PharmacySectionTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.ClientRecordMappers
{
    public static class ClientRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<ClientRecord> GetClientRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ClientRecord());
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
            mapper.Property(x => x.Filler1, 50);
            mapper.ComplexProperty(x => x.PharmacySection, GetPharmacySectionTypeMapper(), 105);
            mapper.ComplexProperty(x => x.DentalSection, GetDentalSectionTypeMapper(), 98);
            return mapper;
        }
    }
}
