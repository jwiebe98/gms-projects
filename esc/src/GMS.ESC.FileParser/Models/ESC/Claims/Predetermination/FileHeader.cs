namespace GMS.ESC.FileParser.Models.ESC.Predetermination
{
    public class FileHeader
    {
        public string RecordIdentifier { get; set; }
        public string IssuerIdentifierNumber { get; set; }
        public string IssuerIdentifierName { get; set; }
        public string DestinationName { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationProvince { get; set; }
        public string DestinationPostalCode { get; set; }
        public string DestinationTelephoneNumber { get; set; }
        public string RunDate { get; set; }
        public string TransmittalSequenceNumber { get; set; }
        public string CutOffDate { get; set; }
        public string Filler1 { get; set; }
        public string ProgramVersion { get; set; }
        public string Filler2 { get; set; }
    }
}
