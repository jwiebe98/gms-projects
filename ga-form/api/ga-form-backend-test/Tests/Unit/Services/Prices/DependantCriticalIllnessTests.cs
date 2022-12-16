using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class DependantCriticalIllnessTests
    {
        public static DependantCriticalIllness CreateDepCIPlan()
        {
            var dependantCriticalIllness = new DependantCriticalIllness
            {
                coverageAmount = CoverageAmount._5000_2500,
                coverageOption = CoverageOption.traditional
            };
            return dependantCriticalIllness;
        }

        private static async Task<Quote> CreateQuoteWithPrices(string employeeType, DependantCriticalIllness? depCIPlan, int numberOfEmployees)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.dependantCriticalIllness = depCIPlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(numberOfEmployees, employeeType, 12345, new List<string>());
            classes.Add(employeeClass);
            quote.classes = classes;
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepCITotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantCriticalIllness.total, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepCIMinLives_Not_Qualified_Single()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateDepCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantCriticalIllness.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepCIMinLives_Qualified_Couple()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepCIPlan(), 1);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantCriticalIllness.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepCIMinLives_Qualified_Family()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.couple, CreateDepCIPlan(), 1);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantCriticalIllness.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepCITotalIsZeroWhenNull()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, null, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantCriticalIllness.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantCriticalIllness.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepCIPlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantCriticalIllness.volume, 3);
        }

    }
}
