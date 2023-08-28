using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental.Mappers
{
    public static class ProviderHeaderTypeMapper
    {
        public static IFixedLengthTypeMapper<ProviderHeader> GetProviderHeaderTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ProviderHeader());
            mapper.Property(x => x.RecordIdentifier, 1);
            mapper.Property(x => x.ProviderNumber, 9);
            mapper.Property(x => x.ProviderOffice, 4);
            mapper.Property(x => x.ServiceProviderSurname, 30);
            mapper.Property(x => x.ServiceProviderFirstName, 30);
            mapper.Property(x => x.ProviderName, 30);
            mapper.Property(x => x.ProviderAddressLine1, 40);
            mapper.Property(x => x.ProviderAddressLine2, 40);
            mapper.Property(x => x.ProviderAddressLine3, 40);
            mapper.Property(x => x.ProviderCity, 35);
            mapper.Property(x => x.ProviderProvince, 2);
            mapper.Property(x => x.ProviderCountry, 15);
            mapper.Property(x => x.ProviderPostalCode, 6);
            mapper.Property(x => x.ProviderTelephoneNumber, 10);
            mapper.Property(x => x.ProviderLanguageFlag, 1);
            mapper.Property(x => x.ProviderEFTRouteCode, 9);
            mapper.Property(x => x.ProviderEFTAccountNumber, 12);
            mapper.Property(x => x.Filler, 4247);
            return mapper;
        }
    }
}
