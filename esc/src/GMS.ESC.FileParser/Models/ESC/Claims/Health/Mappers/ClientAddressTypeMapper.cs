using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers
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
            mapper.Property(x => x.ClientEFTRouteCode, 9);
            mapper.Property(x => x.ClientEFTAccountNumber, 12);
            mapper.Property(x => x.ClientAddressChangeFlag, 1);
            mapper.Property(x => x.GSAS, 19);
            mapper.Property(x => x.PayeeLastName, 30);
            mapper.Property(x => x.PayeeFirstName, 30);
            mapper.Property(x => x.PayeeAddressLine1, 35);
            mapper.Property(x => x.PayeeAddressLine2, 35);
            mapper.Property(x => x.PayeeCity, 35);
            mapper.Property(x => x.PayeeProvince, 2);
            mapper.Property(x => x.PayeeCountry, 15);
            mapper.Property(x => x.PayeePostalCode, 9);
            mapper.Property(x => x.Filler, 4122);
            return mapper;
        }
    }
}
