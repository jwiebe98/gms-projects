using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers
{
    public static class ProviderBatchControlTypeMapper
    {
        public static IFixedLengthTypeMapper<ProviderBatchControl> GetProviderBatchControlTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ProviderBatchControl());
            mapper.Property(x => x.RecordIdentifier, 1);
            mapper.Property(x => x.RecordCount, 6);
            mapper.Property(x => x.Filler1, 64);
            mapper.Property(x => x.ClaimAmount, 8);
            mapper.Property(x => x.ReversalAmount, 8);
            mapper.Property(x => x.AdjustmentAmount, 8);
            mapper.Property(x => x.TotalAmountPayable, 9);
            mapper.Property(x => x.Filler2, 4455);
            return mapper;
        }
    }
}