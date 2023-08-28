using FlatFiles.TypeMapping;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.ClientAddressRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.ClientRecordMappers.ClientRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCClientRecordMappers.EHCClientRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCPatientRecordMappers.EHCPatientRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.FileHeaderTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.FileTotalsTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientAccumulatorRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientExceptionRecordMappers.PatientExceptionRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientRecordMappers.PatientRecordTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers
{
    public static class EligibilityFileMapperTypeSelector
    {
        public static FixedLengthTypeMapperSelector GetEligibilityFileMapperTypeSelector()
        {
            var selector = new FixedLengthTypeMapperSelector();
            selector.When(values => values.StartsWith("00")).Use(GetFileHeaderTypeMapper());
            selector.When(values => values.StartsWith("20") || values.StartsWith("25")).Use(GetClientRecordTypeMapper());
            selector.When(values => values.StartsWith("22") || values.StartsWith("27")).Use(GetPatientRecordTypeMapper());
            selector.When(values => values.StartsWith("30") || values.StartsWith("35")).Use(GetEHCClientRecordTypeMapper());
            selector.When(values => values.StartsWith("32") || values.StartsWith("37")).Use(GetEHCPatientRecordTypeMapper());
            selector.When(values => values.StartsWith("23")).Use(GetPatientAccumulatorRecordTypeMapper());
            selector.When(values => values.StartsWith("24")).Use(GetPatientExceptionRecordTypeMapper());
            selector.When(values => values.StartsWith("29")).Use(GetClientAddressRecordTypeMapper());
            selector.When(values => values.StartsWith("90")).Use(GetFileTotalsTypeMapper());
            return selector;
        }
    }
}
