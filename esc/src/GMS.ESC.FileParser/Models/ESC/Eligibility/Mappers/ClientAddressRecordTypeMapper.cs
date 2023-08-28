using FlatFiles.TypeMapping;

namespace GMS.ESC.FileParser.Models.ESC.Eligibility.Mappers
{
    public static class ClientAddressRecordTypeMapper
    {
        public static IFixedLengthTypeMapper<ClientAddressRecord> GetClientAddressRecordTypeMapper()
        {
            var mapper = FixedLengthTypeMapper.Define(() => new ClientAddressRecord());
            mapper.Property(x => x.RecordType, 2);
            mapper.Property(x => x.CarrierID, 2);
            mapper.Property(x => x.GroupNumber, 10);
            mapper.Property(x => x.ClientID, 15);
            mapper.Property(x => x.ClientLastName, 30);
            mapper.Property(x => x.ClientFirstName, 30);
            mapper.Property(x => x.Address1, 35);
            mapper.Property(x => x.Address2, 35);
            mapper.Property(x => x.City, 35);
            mapper.Property(x => x.Province, 2);
            mapper.Property(x => x.Country, 15);
            mapper.Property(x => x.PostalCode, 6);
            mapper.Property(x => x.Filler, 140);
            return mapper;
        }
    }
}
