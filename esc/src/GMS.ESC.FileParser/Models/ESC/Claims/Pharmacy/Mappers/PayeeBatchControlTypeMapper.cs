using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers
{
    public static class PayeeBatchControlTypeMapper
    {
        public static IFixedLengthTypeMapper<PayeeBatchControl> GetPayeeBatchControlTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PayeeBatchControl());
            mapper.Property(x => x.RecordType, 1);
            mapper.Property(x => x.RecordCount, 6);
            mapper.Property(x => x.SumOfTotalAmount, 8);
            mapper.Property(x => x.HashTotal, 8);
            mapper.Property(x => x.Filler, 727);
            return mapper;
        }
    }
}
