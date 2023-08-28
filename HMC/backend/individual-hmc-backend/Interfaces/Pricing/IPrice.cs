namespace Gmsca.HelpMeChoose.Individual.Interfaces.Pricing
{
    public interface IPrice
    {
        public float MonthlyPremium { get; set; }
        public float AnnualPremium { get; set; }
        public float AdminFee { get; set; }
        public bool ApplicationRequiresReview { get; set; }
    }
}
