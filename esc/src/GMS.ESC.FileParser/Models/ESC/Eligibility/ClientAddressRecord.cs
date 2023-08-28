namespace GMS.ESC.FileParser.Models.ESC.Eligibility
{
    public class ClientAddressRecord
    {
        public string RecordType { get; set; }
        public string CarrierID { get; set; }
        public string GroupNumber { get; set; }
        public string ClientID { get; set; }
        public string ClientLastName { get; set; }
        public string ClientFirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Filler { get; set; }
    }
}
