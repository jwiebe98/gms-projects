namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy
{
    public class FileHeader
    {
        public string RecordType { get; set; }
        public string IssuerIdentifierNumber { get; set; }
        public string IssuerIdentifierName { get; set; }
        public string DestinationName { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationProvince { get; set; }
        public string DestinationPostalCode { get; set; }
        public string DestinationTelephoneNumber { get; set; }
        public string RunDate { get; set; }
        public string CutOffDate { get; set; }
        public string TransmittalSequenceNumber { get; set; }
        public string VersionNumber { get; set; }
        public string Filler { get; set; }
    }
}
