using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.ClientRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.ClientRecordMappers
{
    public static class DentalSectionTypeMapper
    {
        public static IFixedLengthTypeMapper<DentalSection> GetDentalSectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new DentalSection());
            mapper.Property(x => x.DentalProcessingMode, 1);
            mapper.Property(x => x.CarrierDentalField, 10);
            mapper.Property(x => x.DentalRecordEffectiveDate, 8);
            mapper.Property(x => x.DentalRecordExpiryDate, 8);
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
            mapper.Property(x => x.Filler3, 42);
            return mapper;
        }
    }
}
