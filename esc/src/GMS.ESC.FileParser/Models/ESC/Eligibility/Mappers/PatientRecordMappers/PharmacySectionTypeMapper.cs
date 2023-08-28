using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.PatientRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientRecordMappers
{
    public static class PharmacySectionTypeMapper
    {
        public static IFixedLengthTypeMapper<PharmacySection> GetPharmacySectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PharmacySection());
            mapper.Property(x => x.PharmacyProcessingMode, 1);
            mapper.Property(x => x.DrugEffectiveDate, 8);
            mapper.Property(x => x.DrugTerminationDate, 8);
            mapper.Property(x => x.PlanNumber, 5);
            mapper.Property(x => x.PatientBenefitOverrideCode, 5);
            mapper.Property(x => x.COBStatus, 1);
            mapper.Property(x => x.SuspendFlag, 1);
            mapper.Property(x => x.ExceptionFlag, 1);
            mapper.Property(x => x.EnrollmentDate, 8);
            mapper.Property(x => x.Filler, 41);
            return mapper;
        }
    }
}
