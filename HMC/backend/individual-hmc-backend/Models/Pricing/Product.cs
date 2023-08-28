using Gmsca.HelpMeChoose.Individual.Interfaces.Pricing;

namespace Gmsca.HelpMeChoose.Individual.Models.Pricing
{
    public class Product : IProduct
    {
        public string Plan { get; set; } = string.Empty;
        public bool DentalCoverage { get; set; } = false;
        public bool HospitalCash { get; set; } = false;
        public string DrugCoverage { get; set; } = string.Empty;
        public string TravelCoverage { get; set; } = string.Empty;
    }
}
