using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers
{
    public static class FileBatchControlTypeMapper
    {
        public static IFixedLengthTypeMapper<FileBatchControl> GetFileBatchControlTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new FileBatchControl());
            mapper.Property(x => x.RecordType, 1);
            mapper.Property(x => x.IssuerIdentifierNumber, 6);
            mapper.Property(x => x.RecordCount, 8);
            mapper.Property(x => x.GrandTotal, 10);
            mapper.Property(x => x.Filler, 725);
            return mapper;
        }
    }
}
