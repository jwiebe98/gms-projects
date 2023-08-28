using CsvHelper.Configuration.Attributes;
using GMS.WTP.Models.Converters;
using System.Globalization;

namespace GMS.WTP.Models
{
    public class ClaimBordereau
    {
        [Name("Period")]
        [TypeConverter(typeof(PeriodConverter))]
        public string Period { get; set; } = string.Empty;

        [Name("Product")]
        public string Product { get; set; } = string.Empty;

        [Name("Plan")]
        public string Plan { get; set; } = string.Empty;

        [Name("Cover Cause")]
        public string CoverCause { get; set; } = string.Empty;

        [Name("Claim Number")]
        public int ClaimNumber { get; set; }

        [Name("Date Of Loss")]
        [TypeConverter(typeof(DateConverter))]
        public string DateOfLoss { get; set; } = string.Empty;

        [Name("Date Reported")]
        [TypeConverter(typeof(DateConverter))]
        public string DateReported { get; set; } = string.Empty;

        [Name("Policy Number")]
        public string PolicyNumber { get; set; } = string.Empty;

        [Name("Personal ID")]
        public int? PersonalID { get; set; }

        [Name("Departure Date")]
        [TypeConverter(typeof(DateConverter))]
        public string DepartureDate { get; set; } = string.Empty;

        [Name("Return Date")]
        [TypeConverter(typeof(DateConverter))]
        public string ReturnDate { get; set; } = string.Empty;

        [Name("Description")]
        public string Description { get; set; } = string.Empty;

        [Name("Claimant Age")]
        public int ClaimantAge { get; set; }

        [Name("Claimant Province")]
        public string ClaimantProvince { get; set; } = string.Empty;

        [Name("Claimant")]
        public string Claimant { get; set; } = string.Empty;

        [Name("Date Of Birth")]
        [TypeConverter(typeof(DateConverter))]
        public string DateOfBirth { get; set; } = string.Empty;

        [Name("Sex")]
        public string Sex { get; set; } = string.Empty;

        [Name("Actual Paid Movement")]
        public decimal ActualPaidMovement { get; set; }

        [Name("Actual Received Movement")]
        public decimal ActualReceivedMovement { get; set; }

        [Name("Estimated Paid Movement")]
        [TypeConverter(typeof(AccountingFormatConverter))]
        public decimal EstimatedPaidMovement { get; set; }

        [Name("Estimated Received Movement")]
        public decimal EstimatedReceivedMovement { get; set; }

        [Name("Current Status")]
        public string CurrentStatus { get; set; } = string.Empty;

        [Name("Loss Location")]
        public string LossLocation { get; set; } = string.Empty;

        [Ignore]
        public string Timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

        [Ignore]
        public string FileName { get; set; } = string.Empty;

        [Ignore]
        public int RowNumber { get; set; }


        public string ESBMessageID { get; set; } = string.Empty;
    }
}
