using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.EHCClientRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.EHCClientRecordMappers
{
    public static class EHCSectionTypeMapper
    {
        public static IFixedLengthTypeMapper<EHCSection> GetEHCSectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new EHCSection());
            mapper.Property(x => x.EHCProcessingMode, 1);
            mapper.Property(x => x.CarrierEHCField, 10);
            mapper.Property(x => x.EHCRecordEffectiveDate, 8);
            mapper.Property(x => x.EHCRecordExpiryDate, 8);
            mapper.Property(x => x.PlanNumber, 5);
            mapper.Property(x => x.ClientBenefitOverrideCode, 5);
            mapper.Property(x => x.PrivateCOBRuleNumber, 2);
            mapper.Property(x => x.MaxDependantAge, 2);
            mapper.Property(x => x.MaxStudentAge, 2);
            mapper.Property(x => x.GeneralCode, 1);
            mapper.Property(x => x.SuspendFlag, 1);
            mapper.Property(x => x.CoverageCode, 2);
            mapper.Property(x => x.COBStatusCode, 1);
            mapper.Property(x => x.CostPlus, 1);
            mapper.Property(x => x.EDIThresholdAmount, 7);
            mapper.Property(x => x.Filler, 147);
            return mapper;
        }
    }
}
