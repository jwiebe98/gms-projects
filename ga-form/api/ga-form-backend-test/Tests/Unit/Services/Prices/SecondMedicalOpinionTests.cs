using Gmsca.Group.GA.Models;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Prices
{
    [TestClass]
    public class SecondMedicalOpinionTests
    {
        static async Task<Quote> CreateQuoteWithPrices(string employeeType, bool? smoPlan, int numberOfEmployees)
        {
            var quote = QuoteHelper.SetupBasicQuote();
            var classes = new List<EmployeeClass>();
            EmployeeClass employeeClass = new EmployeeClass();
            employeeClass.className = "A";
            employeeClass.benefits.secondMedicalOpinion = smoPlan;
            employeeClass.employees = QuoteHelper.CreateListOfEmployees(numberOfEmployees, employeeType, 12345, new List<string>());
            classes.Add(employeeClass);
            quote.classes = classes;
            var pricingService = PricingServiceHelper.GetPricingService();
            return await pricingService.SetPricesInQuote(quote);
        }

        [TestMethod]
        public async Task SaveQuote_AssertSMOTotalIsCorrect()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", true, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.secondMedicalOpinion.total, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertSMOMinLives()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", true, 1);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.secondMedicalOpinion.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertSMOTotalIsZeroWhenNull()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", null, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.secondMedicalOpinion.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertSMOTotalIsZeroWhenFalse()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", false, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.secondMedicalOpinion.total, 0);
        }

        [TestMethod]
        public async Task SaveQuote_AssertTotalMonthlyPremiumIsCorrect()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", true, 3);
            Assert.AreEqual(quoteWithPrices.totalMonthlyPremium, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertClassPremiumIsCorrect()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", true, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.classPremium, 3);
        }

        [TestMethod]
        public async Task SaveQuote_AssertRateIsCorrect()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", true, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.secondMedicalOpinion.rate, 1);
        }

        [TestMethod]
        public async Task SaveQuote_AssertVolumeIsCorrect()
        {
            var quoteWithPrices = await CreateQuoteWithPrices("single", true, 3);
            Assert.AreEqual(quoteWithPrices.classes[0].prices.secondMedicalOpinion.volume, 3);
        }
    }
}
