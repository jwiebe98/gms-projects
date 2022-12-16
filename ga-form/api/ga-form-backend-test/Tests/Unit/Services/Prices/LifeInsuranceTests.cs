using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class LifeInsuranceTests
    {
        public static LifeInsurance CreateLifePlan(string coverageAmount)
        {
            var lifeInsurance = new LifeInsurance
            {
                coverageAmount = coverageAmount
            };
            return lifeInsurance;
        }

        private async Task<Quote> CreateQuoteWithPrices(string employeeType, LifeInsurance? lifePlan, int numberOfEmployees)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.lifeInsurance = lifePlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(numberOfEmployees, employeeType, 12345, new List<string>());
            classes.Add(employeeClass);
            quote.classes = classes;
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertLifeTotalIsCorrect_Others()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertLifeTotalIsCorrect_1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._1xSalary), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.total, 39);
        }

        [TestMethod]
        public async Task SaveQuote_AssertLifeMinLivesForOtherCoverages()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._10000), 2);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertLifeMinLivesDoesntApplyTo1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._1xSalary), 2);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.total, 26);
        }

        [TestMethod]
        public async Task SaveQuote_AssertLifeTotalIsZeroWhenNull()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, null, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect_1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._1xSalary), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect_1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._1xSalary), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.volume, 39000);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect_Other()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect_Other()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateLifePlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.lifeInsurance.volume, 30000);
        }
    }
}
