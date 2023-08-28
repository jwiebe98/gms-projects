namespace Gmsca.HelpMeChoose.Individual.Interfaces
{
    public interface IRecommendation
    {
        public string PlanName { get; set; } 
        public string PlanType { get; set; } 
        public string PlanDescription { get; set; } 
        public float Price { get; set; }
        public List<string> Options { get; set; } 
        public string BuyNowLink { get; set; } 
    }
}
