using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers
{
    public static class FileTotalsTypeMapper
    {
        public static IFixedLengthTypeMapper<FileTotals> GetFileTotalsTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new FileTotals());
            mapper.Property(x => x.RecordType, 2);
            mapper.Property(x => x.FileType, 2);
            mapper.Property(x => x.PayorANSINumber, 6);
            mapper.Property(x => x.SourceName, 20);
            mapper.Property(x => x.CreationDate, 8);
            mapper.Property(x => x.CreationTime, 4);
            mapper.Property(x => x.RecordCount, 8);
            mapper.Property(x => x.Filler, 307);
            return mapper;
        }
    }
}
