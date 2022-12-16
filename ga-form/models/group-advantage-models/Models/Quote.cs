using Gmsca.Group.GA.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using EmailAddressAttribute = Gmsca.Group.GA.Models.Validation.EmailAddressAttribute;

namespace Gmsca.Group.GA.Models
{
    public class Quote
    {
        public bool isCompleted { get; set; }
        public string timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        public float totalMonthlyPremium { get; set; }
        [NumberOfEmployeesValidator]
        [AssumptionLifeProductValidator]
        public List<EmployeeClass> classes { get; set; } = new();
        public Qualify qualify { get; set; } = new();
        public ContactInfo regionalSalesDirectorInfo { get; set; } = new();
        public ContactInfo brokerInfo { get; set; } = new();
        public string applicationStatus { get; set; } = string.Empty;
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string quoteId { get; set; } = Guid.NewGuid().ToString();
    }

    public class ContactInfo
    {
        public string id { get; set; } = string.Empty;
        [EmailAddress]
        public string emailAddress { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Not a valid phone number")]
        public string phoneNumber { get; set; } = string.Empty;
    }

    public class Qualify
    {
        public BusinessInfo businessInfo { get; set; } = new();
        public EmployeeAdditionalInfo employeeAdditionalInfo { get; set; } = new();
        public PlanInfo planInfo { get; set; } = new();
    }

    public class PlanInfo
    {
        [StringRange(AllowableValues = new[] { "LESS_THAN_1_YEAR", "ONE_TO_TWO_YEAR", "MORE_THAN_2_YEARS", "", null })]
        public string timeWithCurrentCarrier { get; set; } = string.Empty;
    }

    public class BusinessInfo
    {
        public bool isBusinessYearRound { get; set; }
        public float percentageOfOutsideFunding { get; set; }
        public int yearsOfOutsideFunding { get; set; }
        public string businessName { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "SERVICES_MEDICAL_SERVICES", "SERVICES_IT_SERVICES", "SERVICES_SOCIAL_SERVICES_EDUCATION", "SERVICES_SOCIAL_ASSISTANCE", "SERVICES_PROFESSIONAL_SERVICES", "SERVICES_AUTOMOTIVE_MECHANICAL_SERVICES", "SERVICES_RECREATIONAL_SERVICES", "SERVICES_OTHER_SERVICES", "SERVICES_GENERAL_DENTAL_PRACTICE", "SERVICES_SPECIAL_DENTAL_PRACTICE", "MANUFACTURING", "FINANCIAL_INSURANCE_OFFICES", "FINANCIAL_INVESTMENT_FIRMS", "FINANCIAL_REAL_ESTATE_OFFICES", "FINANCIAL_OTHER_FINANCIAL_SERVICES", "CONSTRUCTION_GENERAL_CONTRACTORS", "CONSTRUCTION_SPECIAL_TRADE_CONTRACTORS", "CONSTRUCTION_ROOFING_COMPANIES", "CONSTRUCTION_DEMOLITION", "PUBLIC_ADMIN", "WHOLESALE_TRADE", "RETAIL_FOOD_GENERAL_RETAIL", "RETAIL_FOOD_RESTAURANTS", "RETAIL_FOOD_AUTOMOTIVE_RETAILERS", "RETAIL_FOOD_PHARMACIES", "RETAIL_EXPLOSIVES_CHEMICALS", "RETAIL_MARIJUANA", "TRANSPORT_UTILITY_TRUCKING", "TRANSPORT_UTILITY_UTILITIES", "TRANSPORT_UTILITY_OILFIELD_COMPANIES", "TRANSPORT_UTILITY_OTHER", "AGRICULTURE_FARMING_OPERATIONS", "AGRICULTURE_AGRICULTURAL_SERVICES", "AGRICULTURE_VETERINARY_SERVICES", "AGRICULTURE_OTHER", "", null })]
        public string natureOfBusiness { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "BC", "AB", "SK", "MB", "ON", "NS", "PE", "NL", "YK", "NT", "", null })]
        public string province { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "LESS_THAN_6_MO", "SIX_TO_TWELVE", "MORE_TWELVE", "", null })]
        public string timeInBusiness { get; set; } = string.Empty;
    }
    public class EmployeeAdditionalInfo
    {
        public int employeesNotCoveredByWCB { get; set; }
        public int employeesOutsideOfCanada { get; set; }
        public int employeesRelatedToOwner { get; set; }
        public int numberOfEmployees { get; set; }
    }

    [CriticalIllnessCoverageAmount]
    [EmployerClassValidator]
    public class EmployeeClass
    {
        public Benefits benefits { get; set; } = new();
        public List<Employee> employees { get; set; } = new();
        public Prices prices { get; set; } = new();
        [StringRange(AllowableValues = new[] { "A", "B", "", null })]
        public string className { get; set; } = string.Empty;
        public ClaimsHistory claimsHistory { get; set; } = new();
        public bool isEmployerClass { get; set; }
    }

    public class ClaimsHistory
    {
        public int numberOfEmployeesCurrentlyReceivingDisability { get; set; }
    }

    [PropertiesMustMatch("accidentalDeathAndDismemberment.coverageAmount", "lifeInsurance.coverageAmount")]
    public class Benefits
    {
        public AccidentalDeathAndDismemberment? accidentalDeathAndDismemberment { get; set; }
        public bool? secondMedicalOpinion { get; set; }
        public CriticalIllness? criticalIllness { get; set; }
        public DentalPlan? dentalPlan { get; set; }
        public DependantCriticalIllness? dependantCriticalIllness { get; set; }
        public DependantLifeInsurance? dependantLifeInsurance { get; set; }
        public HealthSpending? healthSpendingAccount { get; set; }
        public LifeInsurance? lifeInsurance { get; set; }
        public LongTermDisability? longTermDisability { get; set; }
        public ShortTermDisability? shortTermDisability { get; set; }
        [StringRange(AllowableValues = new[] { "SILVER", "GOLD", "PLATINUM", "", null })]
        public string? healthPlan { get; set; }
    }

    [WaivedBenefitValidator]
    public class Employee
    {
        [AgeValidator]
        public int age { get; set; }
        [StringArray(new[] { "HEALTH", "DENTAL" })]
        public List<string> benefitsWaived { get; set; } = new();
        public long salary { get; set; }
        public string employeeName { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "SINGLE", "COUPLE", "FAMILY", "", null })]
        public string type { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "MALE", "FEMALE", "", null })]
        public string sex { get; set; } = string.Empty;
        public string occupation { get; set; } = string.Empty;
        public bool isEmployer { get; set; }
    }

    public class DentalPlan
    {
        [StringRange(AllowableValues = new[] { "_500", "_1000", "_1500", "_2000", "", null })]
        public string combinedYearlyMaximum { get; set; } = "_500";
        [StringRange(AllowableValues = new[] { "SILVER", "GOLD", "PLATINUM", "", null })]
        public string tier { get; set; } = string.Empty;
    }

    public class HealthSpending
    {
        public bool carryForward { get; set; }
        [HCSACoverageAmountValidator]
        public long coverageAmount { get; set; }
    }

    public class CriticalIllness : CriticalIllnessBase
    {
        [StringRange(AllowableValues = new[] { "_10000", "_25000", "", null })]
        public string coverageAmount { get; set; } = string.Empty;
    }

    [DependantCriticalIllnessValidator]
    public class DependantCriticalIllness : CriticalIllnessBase
    {
        [StringRange(AllowableValues = new[] { "_5000_2500", "_10000_5000", "_5000", "_10000", "", null })]
        public string coverageAmount { get; set; } = string.Empty;
    }

    public class CriticalIllnessBase
    {
        [StringRange(AllowableValues = new[] { "TRADITIONAL", "HIGH_SEVERITY", "", null })]
        public string coverageOption { get; set; } = string.Empty;
    }

    public class LifeInsurance : LifeADDBase { }

    public class AccidentalDeathAndDismemberment : LifeADDBase { }

    public class LifeADDBase
    {
        [StringRange(AllowableValues = new[] { "_10000", "_25000", "_50000", "_1XSALARY", "", null })]
        public string coverageAmount { get; set; } = string.Empty;
    }

    public class DependantLifeInsurance
    {
        [StringRange(AllowableValues = new[] { "_5000_2500", "_10000_5000", "", null })]
        public string coverageAmount { get; set; } = string.Empty;
    }

    [STDWaitingPeriodValidator]
    public class ShortTermDisability
    {
        public int currentlyOnDisability { get; set; }
        public int recentClaimsAmount { get; set; }
        [StringRange(AllowableValues = new[] { "AGE_65", "", null })]
        public string terminationAge { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "0_DAYS", "7_DAYS", "", null })]
        public string accidentWaitingPeriod { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "MAXIMUM", "EI_MAXIMUM", "", null })]
        public string benefitAmount { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "16_WEEKS", "17_WEEKS", "26_WEEKS", "", null })]
        public string benefitDuration { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "TAXABLE", "NON_TAXABLE", "", null })]
        public string paymentType { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "0_DAYS", "7_DAYS", "", null })]
        public string hospitalizationWaitingPeriod { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "7_DAYS", "", null })]
        public string sicknessWaitingPeriod { get; set; } = "7_DAYS";
    }

    public class LongTermDisability
    {
        public int currentlyOnDisability { get; set; }
        public int recentClaimsAmount { get; set; }
        [StringRange(AllowableValues = new[] { "AGE_65", "", null })]
        public string terminationAge { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "5_YEARS", "AGE_65", "", null })]
        public string benefitDuration { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "TAXABLE", "NON_TAXABLE", "", null })]
        public string paymentType { get; set; } = string.Empty;
        [StringRange(AllowableValues = new[] { "112_DAYS", "119_DAYS", "182_DAYS", "", null })]
        public string waitingPeriod { get; set; } = string.Empty;
    }
}
