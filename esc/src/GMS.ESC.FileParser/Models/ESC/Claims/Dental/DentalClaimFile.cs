namespace GMS.ESC.FileParser.Models.ESC.Claims.Dental
{
    public class DentalClaimFile
    {
        public FileHeader FileHeader { get; set; }
        public ProviderHeader ProviderHeader { get; set; }
        public ClientAddress ClientAddress { get; set; }
        public ClaimRecord Claim { get; set; }
        public ProviderBatchControl ProviderBatchControl { get; set; }
        public ClientBatchControl ClientBatchControl { get; set; }
        public FileBatchControl FileBatchControl { get; set; }
    }
}
