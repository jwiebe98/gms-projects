using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.PatientRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientRecordMappers
{
    public static class DentalSectionTypeMapper
    {
        public static IFixedLengthTypeMapper<DentalSection> GetDentalSectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new DentalSection());
            mapper.Property(x => x.DentalProcessingMode, 1);
            mapper.Property(x => x.DentalRecordEffectiveDate, 8);
            mapper.Property(x => x.DentalRecordExpiryDate, 8);
            mapper.Property(x => x.PlanNumber, 5);
            mapper.Property(x => x.PatientBenefitOverrideCode, 5);
            mapper.Property(x => x.COBStatus, 1);
            mapper.Property(x => x.SuspendFlag, 1);
            mapper.Property(x => x.LateEntrantIndicator, 1);
            mapper.Property(x => x.CostPlus, 1);
            mapper.Property(x => x.PrivateCOBRuleCode, 2);
            mapper.Property(x => x.EDIThresholdAmount, 7);
            mapper.Property(x => x.Filler, 56);
            return mapper;
        }
    }
}
