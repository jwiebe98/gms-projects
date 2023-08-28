namespace GMS.ESC.FileParser.Models.ESC.Predetermination
{
    public class ClientAddress
    {
        public string RecordIdentifier { get; set; }
        public string ClientID { get; set; }
        public string ClientLastName { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientAddressLine1 { get; set; }
        public string ClientAddressLine2 { get; set; }
        public string ClientCity { get; set; }
        public string ClientProvince { get; set; }
        public string ClientCountry { get; set; }
        public string ClientPostalCode { get; set; }
        public string ClientEFTRouteCode { get; set; }
        public string ClientEFTAccountNumber { get; set; }
        public string ClientAddressChangeFlag { get; set; }
        public string GSAS { get; set; }
        public string PayeeLastName { get; set; }
        public string PayeeFirstName { get; set; }
        public string PayeeAddressLine1 { get; set; }
        public string PayeeAddressLine2 { get; set; }
        public string PayeeCity { get; set; }
        public string PayeeProvince { get; set; }
        public string PayeeCountry { get; set; }
        public string PayeePostalCode { get; set; }
        public string Filler { get; set; }
    }
}
