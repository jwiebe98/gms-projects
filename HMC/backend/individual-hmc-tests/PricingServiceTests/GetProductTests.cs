using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Extensions.Logging;
using Moq;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;


namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class GetProductTests
    {
        [TestMethod]
        public void Test_GetProduct_OptionsSetCorrectly()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var product = pricingService.GetProduct(OMNI_PLAN, new() { BASIC_DRUG, DENTAL_CARE, TRAVEL_UP_TO_15_DAYS, "foo", "bar"});

            Assert.AreEqual(OMNI_PLAN, product.Plan);
            Assert.AreEqual(true, product.DentalCoverage);
            Assert.AreEqual(false, product.HospitalCash);
            Assert.AreEqual(BASIC_DRUG, product.DrugCoverage);
            Assert.AreEqual(TRAVEL_UP_TO_15_DAYS, product.TravelCoverage);
        }
    }
}
