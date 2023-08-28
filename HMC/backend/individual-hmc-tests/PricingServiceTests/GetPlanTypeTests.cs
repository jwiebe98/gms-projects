using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Extensions.Logging;
using Moq;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class GetPlanTypeTests
    {
        [TestMethod]
        public void Test_GetPlanType_Omni_Returns_Personal()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var planType = pricingService.GetPlanType("Omni");

            Assert.AreEqual(PERSONAL_HEALTH, planType);
        }

        [TestMethod]
        public void Test_GetPlanType_Extenda_Returns_Personal()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var planType = pricingService.GetPlanType("Extenda");

            Assert.AreEqual(PERSONAL_HEALTH, planType);
        }

        [TestMethod]
        public void Test_GetPlanType_Basic_Returns_Personal()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var planType = pricingService.GetPlanType("Basic");

            Assert.AreEqual(PERSONAL_HEALTH, planType);
        }

        [TestMethod]
        public void Test_GetPlanType_Other_Returns_Replacement()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var planType = pricingService.GetPlanType("foo");

            Assert.AreEqual(REPLACEMENT_HEALTH, planType);
        }

    }
}
