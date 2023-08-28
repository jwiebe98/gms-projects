using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers
{
    public static class PayeeAddressRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<PayeeAddressRecord> GetPayeeAddressRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new PayeeAddressRecord());
            mapper.Property(x => x.RecordType, 1);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.PayeeLastName, 15);
            mapper.Property(x => x.PayeeFirstName, 12);
            mapper.Property(x => x.PayeeAddressLine1, 30);
            mapper.Property(x => x.PayeeAddressLine2, 30);
            mapper.Property(x => x.PayeeCity, 15);
            mapper.Property(x => x.PayeeProvince, 2);
            mapper.Property(x => x.PayeeCountry, 15);
            mapper.Property(x => x.PayeePostalCode, 9);
            mapper.Property(x => x.ClientAddressChangeFlag, 1);
            mapper.Property(x => x.GSAS, 19);
            mapper.Property(x => x.Filler1, 165);
            mapper.Property(x => x.AlternateIdentification, 16);
            mapper.Property(x => x.Filler2, 405);
            return mapper;
        }
    }
}
