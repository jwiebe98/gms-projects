using FlatFiles.TypeMapping;
using GMS.ESC.FileParser.Models.ESC.Predetermination;

namespace GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers
{
    public static class FileHeaderTypeMapper
    {
        public static IFixedLengthTypeMapper<FileHeader> GetFileHeaderTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new FileHeader());
            mapper.Property(x => x.RecordIdentifier, 1);
            mapper.Property(x => x.IssuerIdentifierNumber, 6);
            mapper.Property(x => x.IssuerIdentifierName, 20);
            mapper.Property(x => x.DestinationName, 20);
            mapper.Property(x => x.DestinationAddress, 30);
            mapper.Property(x => x.DestinationCity, 15);
            mapper.Property(x => x.DestinationProvince, 2);
            mapper.Property(x => x.DestinationPostalCode, 6);
            mapper.Property(x => x.DestinationTelephoneNumber, 10);
            mapper.Property(x => x.RunDate, 8);
            mapper.Property(x => x.TransmittalSequenceNumber, 3);
            mapper.Property(x => x.CutOffDate, 8);
            mapper.Property(x => x.Filler1, 56);
            mapper.Property(x => x.ProgramVersion, 128);
            mapper.Property(x => x.Filler2, 934);
            return mapper;
        }
    }
}
