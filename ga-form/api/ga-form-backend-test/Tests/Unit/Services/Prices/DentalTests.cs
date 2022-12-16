using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.Services.Pricing;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class DentalTests
    {

        public static DentalPlan CreateDentalPlan()
        {
            var dentalPlan = new DentalPlan
            {
                tier = CoverageTier.silver,
                combinedYearlyMaximum = CoverageAmount._500
            };
            return dentalPlan;
        }

        private static async Task<Quote> CreateQuoteWithPrices(string employeeType, DentalPlan? dentalPlan, string waive)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.dentalPlan = dentalPlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(3, employeeType, 12345, new List<string>() { waive });
            classes.Add(employeeClass);
            quote.classes = classes;
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDentalSingleTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateDentalPlan(), "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.single.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDentalCoupleTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.couple, CreateDentalPlan(), "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.couple.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDentalFamilyTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDentalPlan(), "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.family.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDentalPlan(), "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDentalPlan(), "");
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertCoupleWaiveVolumeIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.couple, CreateDentalPlan(), CoverageType.dental);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.couple.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertFamilyWaiveVolumeIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDentalPlan(), CoverageType.dental);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.family.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertSingleCantWaive()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateDentalPlan(), CoverageType.dental);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.single.volume, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassTotalIsEmptyWhenNull()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, null, "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateDentalPlan(), "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.single.rate, 10);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalIsZeroWhenNoPlanTierIsSelected()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, new DentalPlan(), "");
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dental.single.total, 0);
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
            employeeClass.benefits.dentalPlan = CreateDentalPlan();
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
