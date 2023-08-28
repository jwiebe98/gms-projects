using CsvHelper.Configuration.Attributes;
using GMS.WTP.Models.Converters;
using System.Globalization;

namespace GMS.WTP.Models
{
    public class PaymentReport
    {
        [Name("Cheque Number")]
        public int ChequeNumber { get; set; }

        [Name("Payment ID")]
        public int PaymentID { get; set; }

        [Name("Claim Number")]
        public int ClaimNumber { get; set; }

        [Name("Personal ID")]
        public int PersonalID { get; set; }

        [Name("Claim Type")]
        public string ClaimType { get; set; } = string.Empty;

        [Name("ICD10")]
        [TypeConverter(typeof(ICD10Converter))]
        public string ICD10 { get; set; } = string.Empty;

        [Name("Payee")]
        public string Payee { get; set; } = string.Empty;

        [Name("Address 1")]
        public string Address1 { get; set; } = string.Empty;

        [Name("Address 2")]
        public string Address2 { get; set; } = string.Empty;

        [Name("City")]
        public string City { get; set; } = string.Empty;

        [Name("Postal Code")]
        public string PostalCode { get; set; } = string.Empty;

        [Name("Province")]
        public string Province { get; set; } = string.Empty;

        [Name("Country")]
        public string Country { get; set; } = string.Empty;

        [Name("Loss Date")]
        [TypeConverter(typeof(DateConverter))]
        public string LossDate { get; set; } = string.Empty;

        [Name("Payment Date")]
        [TypeConverter(typeof(DateConverter))]
        public string PaymentDate { get; set; } = string.Empty;

        [Name("CAD Amount")]
        public decimal CADAmount { get; set; }

        [Name("USD Amount")]
        public decimal USDAmount { get; set; }

        [Name("USD to CAD")]
        public decimal USDtoCAD { get; set; }

        [Name("Total CAD Amount")]
        public decimal TotalCADAmount { get; set; }

        [Ignore]
        public string Timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

        [Ignore]
        public string FileName { get; set; } = string.Empty;

        [Ignore]
        public int RowNumber { get; set; }

        public string ESBMessageID { get; set; } = string.Empty;
    }
}
