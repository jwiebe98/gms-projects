using Gmsca.Group.GA.Backend.Configs;
using Gmsca.Group.GA.Backend.Controllers;
using Gmsca.Group.GA.Backend.Services.Cosmos;
using Gmsca.Group.GA.Backend.Services.Pricing;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Gmsca.Group.GA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Gmsca.Group.GA.Backend.Tests.Integration
{
    [TestClass]
    public class QuoteControllerTests
    {

        private HttpClient _httpClient;

        public QuoteControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [TestMethod]
        public async Task SaveQuoteRoute_MinimumViableQuote_ReturnsOK()
        {
            var quote = new Quote();

            var response = await _httpClient.PostAsJsonAsync("/api/SaveQuote", quote);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task SaveQuoteRoute_BadQuote_ReturnsBadRequest()
        {
            var quote = new Quote();
            quote.qualify.businessInfo.province = "asdf123";

            var response = await _httpClient.PostAsJsonAsync("/api/SaveQuote", quote);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "When saving quote to cosmos an exception was thrown: 500")]
        public async Task SaveQuoteRoute_FailedCosmosSaveThrowsError()
        {
            var quote = new Quote();

            var mockItemResponse = new Mock<ItemResponse<Quote>>();
            mockItemResponse.Setup(x => x.StatusCode).Returns(HttpStatusCode.InternalServerError);

            var mockContainer = new Mock<Container>();
            mockContainer.Setup(x => x.CreateItemAsync(quote, null, null, default)).Returns(Task.FromResult(mockItemResponse.Object));

            var mockDatabase = new Mock<Database>();
            mockDatabase.Setup(x => x.GetContainer("Quotes")).Returns(mockContainer.Object);

            var mockCosmosService = new Mock<ICosmosService>();
            mockCosmosService.Setup(x => x.GetDatabase()).Returns(mockDatabase.Object);

            var mockPricingService = new Mock<IPricingService>();
            mockPricingService.Setup(x => x.SetPricesInQuote(quote)).Returns(Task.FromResult(quote));

            var mockLogger = new Mock<ILogger<QuoteController>>();
            var quoteController = new QuoteController(mockLogger.Object, mockCosmosService.Object, mockPricingService.Object);

            await quoteController.SaveQuote(quote);
        }

        [TestMethod]
        public async Task GetQuoteRoute_FoundReturnQuote()
        {
            var mockCosmosService = new Mock<ICosmosService>();
            Quote quote = new();
            mockCosmosService.Setup(x => x.GetFromDatabase(It.IsAny<string>(), It.IsAny<QueryDefinition>(), It.IsAny<Func<List<Quote>, Quote?>>())).Returns(Task.FromResult<Quote?>(quote));

            var mockPricingService = new Mock<IPricingService>();
            var mockLogger = new Mock<ILogger<QuoteController>>();
            var quoteController = new QuoteController(mockLogger.Object, mockCosmosService.Object, mockPricingService.Object);

            var result = await quoteController.GetQuote("foo");

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetQuoteRoute_NullReturnNotFound()
        {
            var mockCosmosService = new Mock<ICosmosService>();
            mockCosmosService.Setup(x => x.GetFromDatabase(It.IsAny<string>(), It.IsAny<QueryDefinition>(), It.IsAny<Func<List<Quote>, Quote?>>())).Returns(Task.FromResult<Quote?>(null));

            var mockPricingService = new Mock<IPricingService>();
            var mockLogger = new Mock<ILogger<QuoteController>>();
            var quoteController = new QuoteController(mockLogger.Object, mockCosmosService.Object, mockPricingService.Object);

            var result = await quoteController.GetQuote("foo");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetQuoteRoute_AssertLastOrDefault()
        {
            Quote quote1 = new();
            quote1.applicationStatus = "1";
            Quote quote2 = new();
            quote2.applicationStatus = "2";
            List<Quote> quotes = new() { quote1, quote2 };

            var cosmosService = CosmosServiceHelper.GetCosmosServiceThatReturnsMockedData(quotes);

            var mockPricingService = new Mock<IPricingService>();
            var mockLogger = new Mock<ILogger<QuoteController>>();
            var quoteController = new QuoteController(mockLogger.Object, cosmosService, mockPricingService.Object);

            var response = await quoteController.GetQuote("foo");

            var result = (ObjectResult)response;

            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(Quote));
            var quote = (Quote)result.Value;
            Assert.AreEqual(quote.applicationStatus, "2");
        }
    }
}
