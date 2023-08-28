using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Extensions.Logging;
using Moq;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class GetRecommendationTests
    {
        [TestMethod]
        public async Task Test_GetRecommendation_ValuesAreSetCorrectly()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            List<string> options = new() { DENTAL_CARE };

            Quote quote = new() { Applicant = new() { Province = SK, ApplicantAge = 23 }, Questions = new() { NumberPeopleCovered = YOU } };

            var recommendation = await pricingService.GetRecommendation(OMNI_PLAN, options, quote);

            Assert.AreEqual(OMNI_PLAN, recommendation.PlanName);
            Assert.AreEqual(options, recommendation.Options);
        }
    }
}
