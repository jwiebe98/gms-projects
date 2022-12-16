namespace Gmsca.Group.GA.Backend.TestModels
{
    public class TestRates
    {
        public AssumptionLifeProducts ASSUMPTION_LIFE_PRODUCTS { get; set; } = new();
        public DateTime EFFECTIVE_DATE { get; set; } = new DateTime().Date;
        public ProvinceRates PROVINCE_RATES { get; set; } = new();
        public string id { get; set; } = Guid.NewGuid().ToString();
    }

    public class AssumptionLifeProducts
    {
        public CoverageAmounts ACCIDENTAL_DEATH_AND_DISMEMBERMENT { get; set; } = new();
        public CoverageAmounts DEPENDANT_LIFE_INSURANCE { get; set; } = new();
        public CoverageAmounts LIFE_INSURANCE { get; set; } = new();
        public Coverage SECOND_MEDICAL_OPINION { get; set; } = new();
        public CI CRITICAL_ILLNESS { get; set; } = new();
        public CI DEPENDANT_CRITICAL_ILLNESS { get; set; } = new();
    }

    public class CoverageAmounts
    {
        public Coverage? _10000 { get; set; } = new();
        public Coverage? _10000_5000 { get; set; } = new();
        public Coverage? _1XSALARY { get; set; } = new();
        public Coverage? _25000 { get; set; } = new();
        public Coverage? _5000 { get; set; } = new();
        public Coverage? _5000_2500 { get; set; } = new();
        public Coverage? _50000 { get; set; } = new();
    }

    public class CI
    {
        public CoverageAmounts HIGH_SEVERITY { get; set; } = new();
        public CoverageAmounts TRADITIONAL { get; set; } = new();
    }

    public class Coverage
    {
        public float RATE { get; set; } = 1.0f;
        public int MINIMUM_LIVES { get; set; } = 3;
        public long VOLUME { get; set; } = 10000;
    }

    public class PolicyType
    {
        public float COUPLE { get; set; } = 10.0f;
        public float FAMILY { get; set; } = 10.0f;
        public float SINGLE { get; set; } = 10.0f;
    }

    public class PlanTiers
    {
        public PolicyType GOLD { get; set; } = new();
        public PolicyType PLATINUM { get; set; } = new();
        public PolicyType SILVER { get; set; } = new();
    }

    public class DentalTiers
    {
        public CombinedYearlyMaximum GOLD { get; set; } = new();
        public CombinedYearlyMaximum PLATINUM { get; set; } = new();
        public CombinedYearlyMaximum SILVER { get; set; } = new();
    }

    public class CombinedYearlyMaximum
    {
        public PlanTiers _1000 { get; set; } = new();
        public PlanTiers _1500 { get; set; } = new();
        public PlanTiers _2000 { get; set; } = new();
        public PlanTiers _500 { get; set; } = new();
    }

    public class Plans
    {
        public CombinedYearlyMaximum DENTAL { get; set; } = new();
        public PlanTiers HEALTH { get; set; } = new();
    }

    public class ProvinceRates
    {
        public Plans AB { get; set; } = new();
        public Plans BC { get; set; } = new();
        public Plans MB { get; set; } = new();
        public Plans NL { get; set; } = new();
        public Plans NS { get; set; } = new();
        public Plans NT { get; set; } = new();
        public Plans ON { get; set; } = new();
        public Plans PE { get; set; } = new();
        public Plans SK { get; set; } = new();
        public Plans YK { get; set; } = new();
    }

}
