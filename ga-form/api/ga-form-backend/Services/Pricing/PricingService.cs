using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.Models;
using Gmsca.Group.GA.Backend.Services.Rates;
using Gmsca.Group.GA.Models;
using Newtonsoft.Json.Linq;

namespace Gmsca.Group.GA.Backend.Services.Pricing
{
    public class PricingService : IPricingService
    {
        private readonly ILogger<PricingService> _logger;
        private readonly IRateService _rateService;

        private const string EMPTY = "";

        public PricingService(ILogger<PricingService> logger, IRateService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        private static int GROUP_TOTAL_LIVES = 0;
        private static int GROUP_NON_SINGLES = 0;

        public async Task<Quote> SetPricesInQuote(Quote quote)
        {

            SetNumberOfLivesInGroup(quote);

            _logger.LogInformation("Looping over each employee class");
            foreach (EmployeeClass employeeClass in quote.classes)
            {
                await GetPricesForClass(employeeClass, quote.qualify.businessInfo.province);
            }

            _logger.LogInformation("Calculating total monthly premium");
            quote.totalMonthlyPremium = CalculateTotalMonthlyPremium(quote.classes);

            return quote;
        }

        private async Task GetPricesForClass(EmployeeClass employeeClass, string province)
        {
            _logger.LogInformation($"Calculating prices for class {employeeClass.className}");

            if (province is EMPTY)
            {
                _logger.LogInformation("No Province selected, skipping price calculations");
                return;
            }

            _logger.LogInformation("Resetting prices field");
            employeeClass.prices = new Prices();

            _logger.LogInformation("Getting prices");
            employeeClass.prices = await CalculatePrices(employeeClass, province);
        }

        private async Task<Prices> CalculatePrices(EmployeeClass employeeClass, string province)
        {
            _logger.LogInformation("Creating new prices field");
            Prices prices = new();

            _logger.LogInformation("Getting life insurance price");
            prices.lifeInsurance = await GetLifeInsurancePrice(employeeClass);

            _logger.LogInformation("Getting accidental death and dismemberment price");
            prices.accidentalDeathAndDismemberment = await GetAccidentalDeathAndDismembermentPrice(employeeClass);

            _logger.LogInformation("Getting critical illness price");
            prices.criticalIllness = await GetCIPrice(employeeClass);

            _logger.LogInformation("Getting second medical opinion price");
            prices.secondMedicalOpinion = await GetSMOPrice(employeeClass);

            _logger.LogInformation("Getting dependant critical illness price");
            prices.dependantCriticalIllness = await GetDepCIPrice(employeeClass);

            _logger.LogInformation("Getting dependant life insurance price");
            prices.dependantLifeInsurance = await GetDepLifePrice(employeeClass);

            _logger.LogInformation("Getting health price");
            prices.health = await GetHealthPrice(employeeClass, province);

            _logger.LogInformation("Getting dental price");
            prices.dental = await GetDentalPrice(employeeClass, province);

            _logger.LogInformation("Calculating health and dental premium for class");
            prices.healthAndDentalPremium = CalculateHealthAndDentalPremium(prices);

            _logger.LogInformation("Calculating Assumption Life premium for class");
            prices.assumptionLifePremium = CalculateAssumptionLifePremium(prices);

            _logger.LogInformation("Calculating total monthly premium for class");
            prices.classPremium = CalculateClassPremium(prices);

            _logger.LogInformation("Returning prices");
            return prices;
        }

        private static float CalculateTotalMonthlyPremium(List<EmployeeClass> classes) => classes.Count is 0
                ? 0
                : classes.Select(employeeClass => employeeClass.prices.classPremium).ToList().Aggregate((sum, val) => sum + val);

        private static float CalculateClassPremium(Prices prices) => prices.healthAndDentalPremium + prices.assumptionLifePremium;

        private static float CalculateHealthAndDentalPremium(Prices prices)
        {
            float healthTotal = prices.health.single.total + prices.health.couple.total + prices.health.family.total;

            float dentalTotal = prices.dental.single.total + prices.dental.couple.total + prices.dental.family.total;

            return healthTotal + dentalTotal;
        }

        private static float CalculateAssumptionLifePremium(Prices prices) =>
            prices.criticalIllness.total + prices.dependantCriticalIllness.total + prices.lifeInsurance.total + prices.dependantLifeInsurance.total + prices.accidentalDeathAndDismemberment.total + prices.secondMedicalOpinion.total;

        private async Task<Price> GetDepLifePrice(EmployeeClass employeeClass)
        {
            DependantLifeInsurance? depLife = employeeClass.benefits.dependantLifeInsurance;

            if (depLife is null || depLife.coverageAmount is EMPTY)
            {
                return new Price();
            }

            JObject rates = await _rateService.GetEffectiveRates();

            Coverage coverage = rates[CoverageType.assumptionLifeProducts][CoverageType.dependantLifeInsurance][depLife.coverageAmount].ToObject<Coverage>();

            int volume = employeeClass.employees.HaveDependants().Count;

            return GROUP_NON_SINGLES < coverage.MINIMUM_LIVES ? new Price() : new Price(volume, coverage.RATE);
        }

        private async Task<Price> GetDepCIPrice(EmployeeClass employeeClass)
        {
            DependantCriticalIllness? depCI = employeeClass.benefits.dependantCriticalIllness;

            if (depCI is null || depCI.coverageAmount is EMPTY || depCI.coverageOption is EMPTY)
            {
                return new Price();
            }

            JObject rates = await _rateService.GetEffectiveRates();

            Coverage coverage = rates[CoverageType.assumptionLifeProducts][CoverageType.dependantCriticalIllness][depCI.coverageOption][depCI.coverageAmount].ToObject<Coverage>();

            int volume = employeeClass.employees.HaveDependants().Count;

            return GROUP_NON_SINGLES < coverage.MINIMUM_LIVES ? new Price() : new Price(volume, coverage.RATE);
        }

        private async Task<Price> GetSMOPrice(EmployeeClass employeeClass)
        {
            if (employeeClass.benefits.secondMedicalOpinion is null or false)
            {
                return new Price();
            }

            JObject rates = await _rateService.GetEffectiveRates();

            Coverage coverage = rates[CoverageType.assumptionLifeProducts][CoverageType.secondMedicalOpinion].ToObject<Coverage>();

            int volume = employeeClass.employees.Count;

            return GROUP_TOTAL_LIVES < coverage.MINIMUM_LIVES ? new Price() : new Price(volume, coverage.RATE);
        }

        private async Task<Price> GetCIPrice(EmployeeClass employeeClass)
        {
            CriticalIllness? ci = employeeClass.benefits.criticalIllness;

            if (ci is null || ci.coverageAmount is EMPTY || ci.coverageOption is EMPTY)
            {
                return new Price();
            }

            JObject rates = await _rateService.GetEffectiveRates();

            Coverage coverage = rates[CoverageType.assumptionLifeProducts][CoverageType.criticalIllness][ci.coverageOption][ci.coverageAmount].ToObject<Coverage>();

            int numberOfEmployeesWithCI = employeeClass.employees.Count;

            if (GROUP_TOTAL_LIVES < coverage.MINIMUM_LIVES)
            {
                return new Price();
            }

            long volume = coverage.VOLUME * numberOfEmployeesWithCI;

            float total = volume * coverage.RATE / 1000;

            return new Price(volume, coverage.RATE, total);
        }

        private async Task<EmployeeTypesPrices> GetDentalPrice(EmployeeClass employeeClass, string province)
        {
            EmployeeTypesPrices prices = new();

            if (employeeClass.benefits.dentalPlan is null || employeeClass.benefits.dentalPlan.tier is EMPTY)
            {
                return prices;
            }

            JObject rates = await _rateService.GetEffectiveRates();

            JToken dentalRates = rates[CoverageType.provinceRates][province][CoverageType.dental][employeeClass.benefits.dentalPlan.combinedYearlyMaximum][employeeClass.benefits.dentalPlan.tier];

            List<Employee> employeesWithDental = employeeClass.employees.HaveBenefit(CoverageType.dental);

            prices.single = new Price(
                employeesWithDental.IsType(EmployeeType.single).Count,
                dentalRates[EmployeeType.single].ToObject<float>()
                );

            prices.couple = new Price(
                employeesWithDental.IsType(EmployeeType.couple).Count,
                dentalRates[EmployeeType.couple].ToObject<float>()
                );

            prices.family = new Price(
                employeesWithDental.IsType(EmployeeType.family).Count,
                dentalRates[EmployeeType.family].ToObject<float>()
                );

            return prices;
        }

        private async Task<EmployeeTypesPrices> GetHealthPrice(EmployeeClass employeeClass, string province)
        {
            EmployeeTypesPrices prices = new();

            if (employeeClass.benefits.healthPlan is null or EMPTY)
            {
                return prices;
            }

            JObject rates = await _rateService.GetEffectiveRates();

            JToken healthRates = rates[CoverageType.provinceRates][province][CoverageType.health][employeeClass.benefits.healthPlan];

            List<Employee> employeesWithHealth = employeeClass.employees.HaveBenefit(CoverageType.health);

            prices.single = new Price(
                employeesWithHealth.IsType(EmployeeType.single).Count,
                healthRates[EmployeeType.single].ToObject<float>()
                );

            prices.couple = new Price(
                employeesWithHealth.IsType(EmployeeType.couple).Count,
                healthRates[EmployeeType.couple].ToObject<float>()
                );

            prices.family = new Price(
                employeesWithHealth.IsType(EmployeeType.family).Count,
                healthRates[EmployeeType.family].ToObject<float>()
                );

            return prices;
        }

        private async Task<Price> GetLifeInsurancePrice(EmployeeClass employeeClass)
        {
            LifeInsurance? life = employeeClass.benefits.lifeInsurance;

            if (life is null || life.coverageAmount is EMPTY)
            {
                return new Price();
            }

            JObject rates = await _rateService.GetEffectiveRates();

            Coverage coverage = rates[CoverageType.assumptionLifeProducts][CoverageType.lifeInsurance][life.coverageAmount].ToObject<Coverage>();

            return life.coverageAmount is CoverageAmount._1xSalary
                ? Get1xSalaryPrice(employeeClass, coverage.RATE)
                : GetOtherPrice(employeeClass.employees.Count, coverage);
        }

        private async Task<Price> GetAccidentalDeathAndDismembermentPrice(EmployeeClass employeeClass)
        {
            AccidentalDeathAndDismemberment? add = employeeClass.benefits.accidentalDeathAndDismemberment;

            if (add is null || add.coverageAmount is EMPTY)
            {
                return new Price();
            }

            JObject rates = await _rateService.GetEffectiveRates();

            Coverage coverage = rates[CoverageType.assumptionLifeProducts][CoverageType.accidentalDeathAndDismemberment][add.coverageAmount].ToObject<Coverage>();

            return add.coverageAmount is CoverageAmount._1xSalary
                ? Get1xSalaryPrice(employeeClass, coverage.RATE)
                : GetOtherPrice(employeeClass.employees.Count, coverage);
        }

        private static Price GetOtherPrice(int numberOfEmployees, Coverage coverage)
        {
            if (GROUP_TOTAL_LIVES < coverage.MINIMUM_LIVES)
            {
                return new Price();
            }

            long volume = coverage.VOLUME * numberOfEmployees;

            float total = volume * coverage.RATE / 1000;

            return new Price(volume, coverage.RATE, total);
        }

        private static Price Get1xSalaryPrice(EmployeeClass employeeClass, float rate)
        {
            var salaries = employeeClass.employees.Select(employee => employee.salary).ToList();

            for (int i = 0; i < salaries.Count; i++)
            {
                salaries[i] = RoundToTheNearest1000th(salaries[i]);
            }

            long volume = salaries.Aggregate((sum, val) => sum + val);

            float total = volume * rate / 1000;

            return new Price(volume, rate, total);
        }

        private static long RoundToTheNearest1000th(long val) => (long)Math.Ceiling((decimal)(val / 1000f)) * 1000;

        private static void SetNumberOfLivesInGroup(Quote quote)
        {
            IEnumerable<string> types = quote.classes.SelectMany(c => c.employees).Select(e => e.type);
            GROUP_TOTAL_LIVES = types.Count();
            GROUP_NON_SINGLES = types.Where(t => t is not "SINGLE").Count();
        }
    }

    public static class PricingServiceExtentions
    {
        public static List<Employee> HaveDependants(this List<Employee> employees) => employees.FindAll(employee => employee.type is EmployeeType.couple or EmployeeType.family);
        public static List<Employee> HaveBenefit(this List<Employee> employees, string benefit) => employees.FindAll(employee => employee.type is EmployeeType.single || !employee.benefitsWaived.Contains(benefit));
        public static List<Employee> IsType(this List<Employee> employees, string type) => employees.FindAll((employee) => employee.type == type);
    }
}
