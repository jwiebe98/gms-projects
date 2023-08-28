using Gmsca.HelpMeChoose.Individual.Interfaces;

namespace Gmsca.HelpMeChoose.Individual.Models
{
    public class Recommendation : IRecommendation
    {
        public string PlanName { get; set; } = string.Empty;
        public string PlanType { get; set; } = string.Empty;
        public string PlanDescription { get; set; } = string.Empty;
        public float Price { get; set; }
        public List<string> Options { get; set; } = new();
        public string BuyNowLink { get; set; } = string.Empty;
    }
}
