using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class CriticalIllnessTest
    {

        public static CriticalIllness CreateCIPlan()
        {
            var ci = new CriticalIllness
            {
                coverageOption = CoverageOption.traditional,
                coverageAmount = CoverageAmount._25000
            };
            return ci;

        }

        private async Task<Quote> CreateQuoteWithPrices(CriticalIllness? ciPlan, int numberOfEmployees)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.criticalIllness = ciPlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(numberOfEmployees, EmployeeType.single, 12345, new List<string>());
            classes.Add(employeeClass);
            quote.classes = classes;
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertCITotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(CreateCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.criticalIllness.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertCIMinLives()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(CreateCIPlan(), 1);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.criticalIllness.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertCITotalIsZeroWhenNull()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(null, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.criticalIllness.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(CreateCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(CreateCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(CreateCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.criticalIllness.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(CreateCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.criticalIllness.volume, 30000);
        }
    }
}
