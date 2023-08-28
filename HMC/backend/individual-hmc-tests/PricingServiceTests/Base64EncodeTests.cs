using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Azure.Cosmos.Core;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class Base64EncodeTests
    {
        [TestMethod]
        public void Test_Base64Encode()
        {
            var id = "asdf123";

            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string base64EncodedString = pricingService.Base64Encode(new Quote()
            {
                id = id
            });

            byte[] byteArray = Convert.FromBase64String(base64EncodedString);
            string jsonBack = Encoding.UTF8.GetString(byteArray);
            var quoteBack = JsonConvert.DeserializeObject<Quote>(jsonBack);

            Assert.IsNotNull(quoteBack);
            Assert.AreEqual(quoteBack.id, id);
        }
    }
}
