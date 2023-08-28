namespace Gmsca.HelpMeChoose.Individual.Interfaces.Pricing
{
    public interface IProduct
    {
        public string Plan { get; set; }
        public bool DentalCoverage { get; set; }
        public bool HospitalCash { get; set; }
        public string DrugCoverage { get; set; }
        public string TravelCoverage { get; set; }
    }
}
