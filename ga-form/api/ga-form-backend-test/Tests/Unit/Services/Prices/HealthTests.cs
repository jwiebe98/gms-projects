using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.Services.Pricing;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class HealthTests
    {
        private async Task<Quote> CreateQuoteWithPrices(string employeeType, string? healthPlan, string waive)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.healthPlan = healthPlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(3, employeeType, 12345, new List<string>() { waive });
            classes.Add(employeeClass);
            quote.classes = classes;
            PricingService pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertHealthSingleTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CoverageTier.silver, "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.health.single.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertHealthCoupleTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.couple, CoverageTier.silver, "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.health.couple.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertHealthFamilyTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CoverageTier.silver, "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.health.family.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CoverageTier.silver, "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CoverageTier.silver, "");
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertCoupleWaiveVolumeIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.couple, CoverageTier.silver, CoverageType.health);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.health.couple.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertFamilyWaiveVolumeIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CoverageTier.silver, CoverageType.health);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.health.family.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertSingleCantWaive()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CoverageTier.silver, CoverageType.health);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.health.single.volume, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassTotalIsCorrectWithWaive()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CoverageTier.silver, CoverageType.health);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassTotalIsEmptyWhenNull()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, null, "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CoverageTier.silver, "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.health.family.rate, 10);
        }

        [TestMethod]
        public async Task SaveQuote_AssertHealthAndDentalTotalIsCorrect()
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.healthPlan = CoverageTier.silver;
            List<Employee> employees =
                QuoteHelper.CreateListOfEmployees(3, EmployeeType.single, 12345, new List<string>())
                .Concat(QuoteHelper.CreateListOfEmployees(3, EmployeeType.couple, 12345, new List<string>()))
                .Concat(QuoteHelper.CreateListOfEmployees(3, EmployeeType.family, 12345, new List<string>()))
                .ToList();
            employeeClass.employees = employees;
            classes.Add(employeeClass);
            quote.classes = classes;
            PricingService pricingService = PricingServiceHelper.GetPricingService();
            Quote quoteWithPrices = await pricingService.SetPricesInQuote(quote);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.healthAndDentalPremium, 90);
        }
    }
}
