using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class DependantLifeInsuranceTests
    {
        public static DependantLifeInsurance CreateDepLifePlan()
        {
            var dependantLifeInsurance = new DependantLifeInsurance { coverageAmount = CoverageAmount._5000_2500 };
            return dependantLifeInsurance;
        }

        private async Task<Quote> CreateQuoteWithPrices(string employeeType, DependantLifeInsurance? depLifePlan, int numberOfEmployees)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.dependantLifeInsurance = depLifePlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(numberOfEmployees, employeeType, 12345, new List<string>());
            classes.Add(employeeClass);
            quote.classes = classes;
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepLifeTotalIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantLifeInsurance.total, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepLifeMinLives_Not_Qualified_Single()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantLifeInsurance.volume, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepLifeMinLives_Qualified_Couple()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.couple, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantLifeInsurance.volume, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepLifeMinLives_Qualified_Family()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantLifeInsurance.volume, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertDepLifeTotalIsZeroWhenNull()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, null, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantLifeInsurance.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantLifeInsurance.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.family, CreateDepLifePlan(), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.dependantLifeInsurance.volume, 3);
        }
    }
}
