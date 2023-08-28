using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.EHCPatientRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCPatientRecordMappers
{
    public static class EHCSectionTypeMapper
    {
        public static IFixedLengthTypeMapper<EHCSection> GetEHCSectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new EHCSection());
            mapper.Property(x => x.EHCProcessingMode, 1);
            mapper.Property(x => x.EHCRecordEffectiveDate, 8);
            mapper.Property(x => x.EHCRecordExpiryDate, 8);
            mapper.Property(x => x.PlanNumber, 5);
            mapper.Property(x => x.PatientBenefitOverrideCode, 5);
            mapper.Property(x => x.COBStatus, 1);
            mapper.Property(x => x.SuspendFlag, 1);
            mapper.Property(x => x.LateEntrantIndicator, 1);
            mapper.Property(x => x.CostPlus, 1);
            mapper.Property(x => x.PrivateCOBRuleCode, 2);
            mapper.Property(x => x.EDIThresholdAmount, 7);
            mapper.Property(x => x.Filler, 135);
            return mapper;
        }
    }
}
