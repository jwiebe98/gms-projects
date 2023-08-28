using Gmsca.HelpMeChoose.Individual.Interfaces.Pricing;
using System.Globalization;
using static Gmsca.HelpMeChoose.Individual.Constants.Constants;

namespace Gmsca.HelpMeChoose.Individual.Models.Pricing
{
    public class PricingRequest : IPricingRequest
    {
        public string ApplicationDate { get; set; } = DateTime.UtcNow.ToString(ISO_8601_FORMAT, CultureInfo.InvariantCulture);
        public string EffectiveDate { get; set; } = DateTime.UtcNow.ToString(ISO_8601_FORMAT, CultureInfo.InvariantCulture);
        public string Province { get; set; } = string.Empty;
        public List<Applicant> Applicants { get; set; } = new();
        public Product Product { get; set; } = new();

    }
}
