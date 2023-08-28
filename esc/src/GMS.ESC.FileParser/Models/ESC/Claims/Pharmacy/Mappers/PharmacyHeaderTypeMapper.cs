using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers
{
    public static class PharmacyHeaderTypeMapper
    {
        public static IFixedLengthTypeMapper<PharmacyHeader> GetPharmacyHeaderTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PharmacyHeader());
            mapper.Property(x => x.RecordType, 1);
            mapper.Property(x => x.PharmacyNumber, 10);
            mapper.Property(x => x.PharmacyName, 30);
            mapper.Property(x => x.PharmacyAddressLine1, 35);
            mapper.Property(x => x.PharmacyAddressLine2, 35);
            mapper.Property(x => x.PharmacyAddressLine3, 35);
            mapper.Property(x => x.PharmacyProvince, 2);
            mapper.Property(x => x.PharmacyPostalCode, 6);
            mapper.Property(x => x.PharmacyTelephoneNumber, 10);
            mapper.Property(x => x.PharmacyLanguageFlag, 1);
            mapper.Property(x => x.PharmacyPayDirectionFlag, 1);
            mapper.Property(x => x.PharmacyChainNumber, 10);
            mapper.Property(x => x.ESICanadaPharmacyFlag, 1);
            mapper.Property(x => x.PharmacyEFTRouteCode, 9);
            mapper.Property(x => x.PharmacyEFTAccountNumber, 12);
            mapper.Property(x => x.Filler, 552);
            return mapper;
        }
    }
}
