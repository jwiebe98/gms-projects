using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers
{
    public static class PatientAccumulatorRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<PatientAccumulatorRecord> GetPatientAccumulatorRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PatientAccumulatorRecord());
            mapper.Property(x => x.RecordType, 2);
            mapper.Property(x => x.CarrierID, 2);
            mapper.Property(x => x.GroupNumber, 10);
            mapper.Property(x => x.Filler1, 9);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.PatientCode, 3);
            mapper.Property(x => x.EffectiveDate, 8);
            mapper.Property(x => x.AccumulatorInstruction, 1);
            mapper.Property(x => x.AccumulatorType, 2);
            mapper.Property(x => x.AccumulatorAmount, 7);
            mapper.Property(x => x.CategoryCode, 2);
            mapper.Property(x => x.AccumulatorID, 5);
            mapper.Property(x => x.ProviderNumber, 9);
            mapper.Property(x => x.TransReferenceNumber, 14);
            mapper.Property(x => x.ProcedurePaidLineNumber, 2);
            mapper.Property(x => x.ToothCode, 2);
            mapper.Property(x => x.BenefitYearRolloverDate, 8);
            mapper.Property(x => x.Filler2, 256);
            return mapper;
        }
    }
}
