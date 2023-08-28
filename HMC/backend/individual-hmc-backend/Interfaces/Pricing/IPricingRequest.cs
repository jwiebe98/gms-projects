using Gmsca.HelpMeChoose.Individual.Models.Pricing;
using System.Globalization;

namespace Gmsca.HelpMeChoose.Individual.Interfaces.Pricing
{
    public interface IPricingRequest
    {
        public string ApplicationDate { get; set; }
        public string EffectiveDate { get; set; }
        public string Province { get; set; }
        public List<Applicant> Applicants { get; set; }
        public Product Product { get; set; }
    }
}
