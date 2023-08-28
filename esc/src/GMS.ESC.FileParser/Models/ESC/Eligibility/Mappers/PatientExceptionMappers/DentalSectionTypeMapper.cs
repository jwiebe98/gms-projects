using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Eligibility.PatientExceptionRecordRow;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers.PatientExceptionRecordMappers
{
    public static class DentalSectionTypeMapper
    {
        public static IFixedLengthTypeMapper<DentalSection> GetDentalSectionTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new DentalSection());
            mapper.Property(x => x.DentalProcessingMode, 1);
            mapper.Property(x => x.EffectiveDate, 8);
            mapper.Property(x => x.ExpiryDate, 8);
            mapper.Property(x => x.ProcedureCode, 5);
            mapper.Property(x => x.ProcedureCodeSource, 4);
            mapper.Property(x => x.LabLimit, 6);
            mapper.Property(x => x.ExpenseLimit, 6);
            mapper.Property(x => x.CategoryCode, 2);
            mapper.Property(x => x.FrequencyID1, 5);
            mapper.Property(x => x.FrequencyID2, 5);
            mapper.Property(x => x.FrequencyID3, 5);
            mapper.Property(x => x.FrequencyID4, 5);
            mapper.Property(x => x.MaterialIntervention, 2);
            mapper.Property(x => x.IncludeExclude, 1);
            mapper.Property(x => x.Filler, 75);
            return mapper;
        }
    }
}
