namespace GMS.ESC.FileParser.Models.ESC.Eligibility
{
    public class FileHeader
    {
        public string RecordType { get; set; }
        public string FileType { get; set; }
        public string PayorANSINumber { get; set; }
        public string SourceName { get; set; }
        public string CreationDate { get; set; }
        public string CreationTime { get; set; }
        public string TransmittalSequenceNumber { get; set; }
        public string FormatVersion { get; set; }
        public string LoadType { get; set; }
        public string Comment { get; set; }
        public string TPAIndicator { get; set; }
        public string Filler { get; set; }
    }
}
