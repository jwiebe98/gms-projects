using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers
{
    public static class ClientBatchControlTypeMapper
    {
        public static IFixedLengthTypeMapper<ClientBatchControl> GetClientBatchControlTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ClientBatchControl());
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
