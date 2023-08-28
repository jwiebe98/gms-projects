namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy
{
    public class PharmacyClaimFile
    {
        public FileHeader FileHeader { get; set; }
        public PharmacyHeader ProviderHeader { get; set; }
        public PayeeAddressRecord ClientAddress { get; set; }
        public ClaimRecord Claim { get; set; }
        public PharmacyBatchControl ProviderBatchControl { get; set; }
        public PayeeBatchControl ClientBatchControl { get; set; }
        public FileBatchControl FileBatchControl { get; set; }
    }
}
