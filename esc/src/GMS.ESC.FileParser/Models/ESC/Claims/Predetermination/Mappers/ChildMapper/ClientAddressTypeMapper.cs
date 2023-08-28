using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers
{
    public static class ClientAddressTypeMapper
    {
        public static IFixedLengthTypeMapper<ClientAddress> GetClientAddressTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ClientAddress());
            mapper.Property(x => x.RecordIdentifier, 1);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.ClientLastName, 30);
            mapper.Property(x => x.ClientFirstName, 30);
            mapper.Property(x => x.ClientAddressLine1, 35);
            mapper.Property(x => x.ClientAddressLine2, 35);
            mapper.Property(x => x.ClientCity, 35);
            mapper.Property(x => x.ClientProvince, 2);
            mapper.Property(x => x.ClientCountry, 15);
            mapper.Property(x => x.ClientPostalCode, 9);
            mapper.Property(x => x.GSAS, 19);
            mapper.Property(x => x.Filler, 1021);
            return mapper;
        }
    }
}
