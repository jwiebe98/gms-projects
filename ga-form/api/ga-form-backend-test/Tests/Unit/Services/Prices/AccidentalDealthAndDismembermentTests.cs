using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class AccidentalDealthAndDismembermentTests
    {
        public static AccidentalDeathAndDismemberment CreateADDPlan(string coverageAmount)
        {
            var accidentalDeathAndDismemberment = new AccidentalDeathAndDismemberment
            {
                coverageAmount = coverageAmount
            };
            return accidentalDeathAndDismemberment;
        }

        private async Task<Quote> CreateQuoteWithPrices(string employeeType, AccidentalDeathAndDismemberment? addPlan, int numberOfEmployees)
        {
            Quote quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new()
            {
                className = "A"
            };
            employeeClass.benefits.accidentalDeathAndDismemberment = addPlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(numberOfEmployees, employeeType, 12345, new List<string>());
            classes.Add(employeeClass);
            quote.classes = classes;
            Backend.Services.Pricing.PricingService pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertADDTotalIsCorrect_Others()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.total, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertADDTotalIsCorrect_1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._1xSalary), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.total, 39);
        }

        [TestMethod]
        public async Task SaveQuote_AssertADDMinLives()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._10000), 1);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertADDMinLivesDoesntApplyTo1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._1xSalary), 1);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.total, 13);
        }

        [TestMethod]
        public async Task SaveQuote_AssertADDTotalIsZeroWhenNull()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, null, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassPremiumIsCorrect()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 30);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect_1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._1xSalary), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect_1xSalary()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._1xSalary), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.volume, 39000);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect_Other()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect_Other()
        {
            Quote quoteWithPrices = await CreateQuoteWithPrices(EmployeeType.single, CreateADDPlan(CoverageAmount._10000), 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.accidentalDeathAndDismemberment.volume, 30000);
        }
    }
}
