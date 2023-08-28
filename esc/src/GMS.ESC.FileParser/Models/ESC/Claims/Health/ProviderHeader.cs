namespace GMS.ESC.FileParser.Models.ESC.Claims.Health
{
    public class ProviderHeader
    {
        public string RecordIdentifier { get; set; }
        public string ProviderNumber { get; set; }
        public string ProviderOffice { get; set; }
        public string ServiceProviderSurname { get; set; }
        public string ServiceProviderFirstName { get; set; }
        public string ProviderName { get; set; }
        public string ProviderAddressLine1 { get; set; }
        public string ProviderAddressLine2 { get; set; }
        public string ProviderAddressLine3 { get; set; }
        public string ProviderCity { get; set; }
        public string ProviderProvince { get; set; }
        public string ProviderCountry { get; set; }
        public string ProviderPostalCode { get; set; }
        public string ProviderTelephoneNumber { get; set; }
        public string ProviderLanguageFlag { get; set; }
        public string ProviderEFTRouteCode { get; set; }
        public string ProviderEFTAccountNumber { get; set; }
        public string Filler { get; set; }
    }
}
