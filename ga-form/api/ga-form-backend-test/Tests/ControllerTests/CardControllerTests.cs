using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebUtilities;

namespace Gmsca.Group.GA.Backend.Tests.ControllerTests
{
    [TestClass]
    public class CardControllerTests
    {
        private HttpClient _httpClient;

        public CardControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task GetCardPricesRoute_CorrectPropertiesReturnsOK()
        {
            Dictionary<string, string?> parameters = new()
            {
                { "province", "SK" },
                { "dentalSilverCombinedYearlyMaximum", "_500" },
                { "dentalGoldCombinedYearlyMaximum", "_1000" },
                { "dentalPlatinumCombinedYearlyMaximum", "_1500" }
            };
            var uri = QueryHelpers.AddQueryString("/api/GetCardPrices", parameters);
            var response = await _httpClient.GetAsync(uri);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task GetCardPricesRoute_IncorrectPropertiesReturnsNonSuccess()
        {
            Dictionary<string, string?> parameters = new();
            var uri = QueryHelpers.AddQueryString("/api/GetCardPrices", parameters);
            var response = await _httpClient.GetAsync(uri);
            Assert.IsTrue(!response.IsSuccessStatusCode);
        }
    }
}
