using Gmsca.Group.GA.Backend.Models;
using Gmsca.Group.GA.Backend.Services.Cosmos;
using Gmsca.Group.GA.Backend.Services.Rates;
using Gmsca.Group.GA.Backend.TestModels;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Microsoft.Azure.Cosmos;
using Moq;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.Rates
{
    [TestClass]
    public class RateServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception),
            "No rates found")]
        public async Task GetEffectiveRates_NullRateThrowsError()
        {
            var mockCosmosService = new Mock<ICosmosService>();
            _ = mockCosmosService.Setup(x => x.GetFromDatabase(It.IsAny<string>(), It.IsAny<QueryDefinition>(), It.IsAny<Func<List<Rate>, Rate?>>())).Returns(Task.FromResult<Rate?>(null));

            var rateService = new RateService(mockCosmosService.Object);
            _ = await rateService.GetEffectiveRates();
        }

        [TestMethod]
        public async Task GetEffectiveRates_AssertRateIsNotInFutureAndNotOlderThan1Year()
        {
            Rate rate1 = new()
            {
                id = "rate older than 1 year",
                EFFECTIVE_DATE = DateTime.Now.AddDays(-366).Date
            };
            Rate rate2 = new()
            {
                id = "effective rate",
                EFFECTIVE_DATE = DateTime.Now.Date
            };
            Rate rate3 = new()
            {
                id = "rate slightly older",
                EFFECTIVE_DATE = DateTime.Now.AddHours(1).Date
            };

            List<Rate> rates = new() { rate1, rate2, rate3 };

            CosmosService cosmosService = CosmosServiceHelper.GetCosmosServiceThatReturnsMockedData(rates);

            var rateService = new RateService(cosmosService);

            Newtonsoft.Json.Linq.JObject effectiveRate = await rateService.GetEffectiveRates();
            Assert.AreEqual(rate2.id, effectiveRate["id"].ToString());
        }
    }
}
