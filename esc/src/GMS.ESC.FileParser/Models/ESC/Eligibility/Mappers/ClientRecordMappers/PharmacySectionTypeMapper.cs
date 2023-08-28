using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.ClientRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.ClientRecordMappers
{
    public static class PharmacySectionTypeMapper
    {
        public static IFixedLengthTypeMapper<PharmacySection> GetPharmacySectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PharmacySection());
            mapper.Property(x => x.PharmacyProcessingMode, 1);
            mapper.Property(x => x.CarrierPharmacyField, 10);
            mapper.Property(x => x.DrugEffectiveDate, 8);
            mapper.Property(x => x.DrugTerminationDate, 8);
            mapper.Property(x => x.PlanNumber, 5);
            mapper.Property(x => x.ClientBenefitOverrideCode, 5);
            mapper.Property(x => x.DURFlag, 1);
            mapper.Property(x => x.ProvincialCOBRuleNumber, 1);
            mapper.Property(x => x.RAMQOverrideFlag, 1);
            mapper.Property(x => x.CutbackOverrideIndicator, 1);
            mapper.Property(x => x.MandatoryGenericOverrideIndicator, 1);
            mapper.Property(x => x.TherapeuticReferenceNumberOverride, 4);
            mapper.Property(x => x.MaxDependantAge, 2);
            mapper.Property(x => x.MaxStudentAge, 2);
            mapper.Property(x => x.GeneralCode, 1);
            mapper.Property(x => x.SuspendFlag, 1);
            mapper.Property(x => x.CoverageCode, 2);
            mapper.Property(x => x.COBStatusCode, 1);
            mapper.Property(x => x.Filler2, 50);
            return mapper;
        }
    }
}
