namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental
{
    public class FileBatchControl
    {
        public string RecordIdentifier { get; set; }
        public string IssuerIdentifierNumber { get; set; }
        public string RecordCount { get; set; }
        public string Filler1 { get; set; }
        public string ClaimAmount { get; set; }
        public string ReversalAmount { get; set; }
        public string AdjustmentAmount { get; set; }
        public string TotalAmountPayable { get; set; }
        public string Filler2 { get; set; }
    }
}
