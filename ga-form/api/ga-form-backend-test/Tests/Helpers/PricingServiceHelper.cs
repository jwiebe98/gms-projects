using Gmsca.Group.GA.Backend.Services.Pricing;
using Gmsca.Group.GA.Backend.Services.Rates;
using Gmsca.Group.GA.Backend.TestModels;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;

namespace Gmsca.Group.GA.Backend.Tests.Helpers
{
    public static class PricingServiceHelper
    {
        public static PricingService GetPricingService()
        {
            var testRates = JObject.FromObject(new TestRates());

            var mockRateService = new Mock<IRateService>();
            mockRateService.Setup(x => x.GetEffectiveRates()).Returns(Task.FromResult(testRates));

            var mockLogger = new Mock<ILogger<PricingService>>();

            return new PricingService(mockLogger.Object, mockRateService.Object);
        }
    }
}
