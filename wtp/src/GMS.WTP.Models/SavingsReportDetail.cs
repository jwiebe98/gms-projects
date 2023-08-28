using CsvHelper.Configuration.Attributes;
using GMS.WTP.Models.Converters;
using System.Globalization;

namespace GMS.WTP.Models
{
    public class SavingsReportDetail
    {
        [Name("Claim Number")]
        public int ClaimNumber { get; set; }

        [Name("Cost Agent")]
        public string CostAgent { get; set; } = string.Empty;

        [Name("Gender")]
        public string Gender { get; set; } = string.Empty;

        [Name("Considered Charges")]
        public decimal ConsideredCharges { get; set; }

        [Name("Contracted Savings")]
        public decimal ContractedSavings { get; set; }

        [Name("Scrubbed Savings")]
        public decimal ScrubbedSavings { get; set; }

        [Name("Paid Amount")]
        public decimal PaidAmount { get; set; }

        [Name("Gross Savings %")]
        [TypeConverter(typeof(PercentageConverter))]
        public decimal GrossSavingsPercent { get; set; }

        [Name("PPO Fees")]
        public decimal PPOFees { get; set; }

        [Name("Total Paid Amount (Paid + Fees)")]
        public decimal TotalPaidAmount { get; set; }

        [Name("Net Savings")]
        public decimal NetSavings { get; set; }

        [Name("Net Savings %")]
        [TypeConverter(typeof(PercentageConverter))]
        public decimal NetSavingsPercent { get; set; }

        [Name("Gross Savings YTD")]
        public decimal GrossSavingsYTD { get; set; }

        [Name("Gross Savings % YTD")]
        [TypeConverter(typeof(PercentageConverter))]
        public decimal GrossSavingsPercentYTD { get; set; }

        [Name("Net Savings YTD")]
        public decimal NetSavingsYTD { get; set; }

        [Name("Net Savings % YTD")]
        [TypeConverter(typeof(PercentageConverter))]
        public decimal NetSavingsPercentYTD { get; set; }

        [Name("Recoveries YTD")]
        public decimal RecoveriesYTD { get; set; }

        [Ignore]
        public string Timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

        [Ignore]
        public string FileName { get; set; } = string.Empty;

        [Ignore]
        public int RowNumber { get; set; }

        public string ESBMessageID { get; set; } = string.Empty;
    }
}
