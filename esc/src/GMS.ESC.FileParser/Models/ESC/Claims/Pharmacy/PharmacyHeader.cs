namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy
{
    public class PharmacyHeader
    {
        public string RecordType { get; set; }
        public string PharmacyNumber { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyAddressLine1 { get; set; }
        public string PharmacyAddressLine2 { get; set; }
        public string PharmacyAddressLine3 { get; set; }
        public string PharmacyProvince { get; set; }
        public string PharmacyPostalCode { get; set; }
        public string PharmacyTelephoneNumber { get; set; }
        public string PharmacyLanguageFlag { get; set; }
        public string PharmacyPayDirectionFlag { get; set; }
        public string PharmacyChainNumber { get; set; }
        public string ESICanadaPharmacyFlag { get; set; }
        public string PharmacyEFTRouteCode { get; set; }
        public string PharmacyEFTAccountNumber { get; set; }
        public string Filler { get; set; }
    }
}
