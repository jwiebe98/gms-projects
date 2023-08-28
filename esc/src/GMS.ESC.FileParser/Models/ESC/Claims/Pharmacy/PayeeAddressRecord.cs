namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy
{
    public class PayeeAddressRecord
    {
        public string RecordType { get; set; }
        public string ClientID { get; set; }
        public string PayeeLastName { get; set; }
        public string PayeeFirstName { get; set; }
        public string PayeeAddressLine1 { get; set; }
        public string PayeeAddressLine2 { get; set; }
        public string PayeeCity { get; set; }
        public string PayeeProvince { get; set; }
        public string PayeeCountry { get; set; }
        public string PayeePostalCode { get; set; }
        public string ClientAddressChangeFlag { get; set; }
        public string GSAS { get; set; }
        public string Filler1 { get; set; }
        public string AlternateIdentification { get; set; }
        public string Filler2 { get; set; }
    }
}
