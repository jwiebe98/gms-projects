using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class TotalMonthlyPremiumTests
    {
        private static Quote CreateQuote(string coverageAmount)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            foreach (string className in new List<string>() { "A", "B" })
            {
                EmployeeClass employeeClass = new()
                {
                    className = className
                };
                employeeClass.benefits.accidentalDeathAndDismemberment = AccidentalDealthAndDismembermentTests.CreateADDPlan(coverageAmount);
                employeeClass.benefits.criticalIllness = CriticalIllnessTest.CreateCIPlan();
                employeeClass.benefits.dentalPlan = DentalTests.CreateDentalPlan();
                employeeClass.benefits.dependantCriticalIllness = DependantCriticalIllnessTests.CreateDepCIPlan();
                employeeClass.benefits.dependantLifeInsurance = DependantLifeInsuranceTests.CreateDepLifePlan();
                employeeClass.benefits.healthPlan = CoverageTier.platinum;
                employeeClass.benefits.lifeInsurance = LifeInsuranceTests.CreateLifePlan(coverageAmount);
                employeeClass.benefits.secondMedicalOpinion = true;
                employeeClass.employees =
                    QuoteHelper.CreateListOfEmployees(3, EmployeeType.single, 12345, new List<string>())
                    .Concat(QuoteHelper.CreateListOfEmployees(1, EmployeeType.couple, 12345, new List<string>()))
                    .Concat(QuoteHelper.CreateListOfEmployees(1, EmployeeType.family, 12345, new List<string>()))
                    .ToList();
                classes.Add(employeeClass);
            }
            quote.classes = classes;
            return quote;
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect_10000()
        {
            Quote quote = CreateQuote(CoverageAmount._10000);
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            Quote quoteWithPrices = await pricingService.SetPricesInQuote(quote);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 518);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect_1xSalary()
        {
            Quote quote = CreateQuote(CoverageAmount._1xSalary);
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            Quote quoteWithPrices = await pricingService.SetPricesInQuote(quote);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 578);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect_NoProvince()
        {
            Quote quote = CreateQuote(CoverageAmount._1xSalary);
            quote.qualify.businessInfo.province = string.Empty;
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            Quote quoteWithPrices = await pricingService.SetPricesInQuote(quote);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect_NoClasses()
        {
            Quote quote = CreateQuote(CoverageAmount._1xSalary);
            quote.classes = new List<EmployeeClass>();
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            Quote quoteWithPrices = await pricingService.SetPricesInQuote(quote);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertAssumptionLifePremiumIsCorrect()
        {
            Quote quote = CreateQuote(CoverageAmount._10000);
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            Quote quoteWithPrices = await pricingService.SetPricesInQuote(quote);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.assumptionLifePremium, 159);
        }
    }
}
