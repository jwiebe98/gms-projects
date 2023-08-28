using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers
{
    public static class FileHeaderTypeMapper
    {
        public static IFixedLengthTypeMapper<FileHeader> GetFileHeaderTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new FileHeader());
            mapper.Property(x => x.RecordType, 2);
            mapper.Property(x => x.FileType, 2);
            mapper.Property(x => x.PayorANSINumber, 6);
            mapper.Property(x => x.SourceName, 20);
            mapper.Property(x => x.CreationDate, 8);
            mapper.Property(x => x.CreationTime, 4);
            mapper.Property(x => x.TransmittalSequenceNumber, 3);
            mapper.Property(x => x.FormatVersion, 2);
            mapper.Property(x => x.LoadType, 4);
            mapper.Property(x => x.Comment, 8);
            mapper.Property(x => x.TPAIndicator, 3);
            mapper.Property(x => x.Filler, 295);
            return mapper;
        }
    }
}
