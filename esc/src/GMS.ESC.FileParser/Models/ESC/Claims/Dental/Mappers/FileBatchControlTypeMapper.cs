using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers
{
    public static class FileBatchControlTypeMapper
    {
        public static IFixedLengthTypeMapper<FileBatchControl> GetFileBatchControlTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new FileBatchControl());
            mapper.Property(x => x.RecordIdentifier, 1);
            mapper.Property(x => x.IssuerIdentifierNumber, 6);
            mapper.Property(x => x.RecordCount, 8);
            mapper.Property(x => x.Filler1, 64);
            mapper.Property(x => x.ClaimAmount, 10);
            mapper.Property(x => x.ReversalAmount, 10);
            mapper.Property(x => x.AdjustmentAmount, 10);
            mapper.Property(x => x.TotalAmountPayable, 12);
            mapper.Property(x => x.Filler2, 4438);
            return mapper;
        }
    }
}
