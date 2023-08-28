using Gmsca.HelpMeChoose.Individual.Interfaces.Pricing;

namespace Gmsca.HelpMeChoose.Individual.Models.Pricing
{
    public class Price : IPrice
    {
        public float MonthlyPremium { get; set; }
        public float AnnualPremium { get; set; }
        public float AdminFee { get; set; }
        public bool ApplicationRequiresReview { get; set; }
    }
}
