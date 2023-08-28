using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.PatientExceptionRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientExceptionRecordMappers
{
    public static class PharmacySectionTypeMapper
    {
        public static IFixedLengthTypeMapper<PharmacySection> GetPharmacySectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PharmacySection());
            mapper.Property(x => x.PharmacyProcessingMode, 1);
            mapper.Property(x => x.EffectiveDate, 8);
            mapper.Property(x => x.ExpiryDate, 8);
            mapper.Property(x => x.DrugLevel, 2);
            mapper.Property(x => x.DIN, 8);
            mapper.Property(x => x.RAMQExceptionCode, 4);
            mapper.Property(x => x.Filler, 2);
            mapper.Property(x => x.GPIndicator, 1);
            mapper.Property(x => x.EclipseCode, 2);
            mapper.Property(x => x.TherapeuticClass, 6);
            mapper.Property(x => x.SeniorsFlag, 1);
            mapper.Property(x => x.ProvincialScheduleCode, 2);
            mapper.Property(x => x.FederalScheduleCode, 1);
            mapper.Property(x => x.IncludeExcludeFlag, 1);
            mapper.Property(x => x.ExceptionDaysSupplyReg, 4);
            mapper.Property(x => x.ExceptionDaysSupplyMaint, 4);
            mapper.Property(x => x.ExceptOverrideCode, 5);
            mapper.Property(x => x.AccumID, 5);
            mapper.Property(x => x.AQPPCode, 3);
            mapper.Property(x => x.Filler, 5);
            mapper.Property(x => x.CutbackOverrideIndicator, 1);
            mapper.Property(x => x.MandatoryGenericOverrideIndicator, 1);
            mapper.Property(x => x.TherapeuticReferenceNumberOverride, 4);
            mapper.Property(x => x.RAMQFlag, 1);
            mapper.Property(x => x.DiseaseCode, 6);
            mapper.Property(x => x.Filler, 92);
            return mapper;
        }
    }
}
