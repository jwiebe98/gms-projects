using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers
{
    public static class PharmacyBatchControlTypeMapper
    {
        public static IFixedLengthTypeMapper<PharmacyBatchControl> GetPharmacyBatchControlTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PharmacyBatchControl());
            mapper.Property(x => x.RecordType, 1);
            mapper.Property(x => x.RecordCount, 6);
            mapper.Property(x => x.SumOfTotalAmount, 8);
            mapper.Property(x => x.HashTotal, 8);
            mapper.Property(x => x.Filler, 722);
            return mapper;
        }
    }
}