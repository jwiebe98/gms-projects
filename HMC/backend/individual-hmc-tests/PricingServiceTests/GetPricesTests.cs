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
    public class GetPricesTests
    {
        private Quote quote = new()
        {
            Applicant = new()
            {
                ApplicantAge = 23,
                SpouseAge = 23,
                Province = "AB"
            },
            Questions = new()
            {
                NumberPeopleCovered = YOU_YOUR_SPOUSE_YOUR_CHILDREN
            }
        };

        private PricingService GetPricingService()
        {
            var recommendationMock = new Mock<IRecommendationService>();
            recommendationMock.Setup(q => q.GetPrimaryRecommendation(It.IsAny<Quote>())).Returns(BASIC_PLAN);
            recommendationMock.Setup(q => q.GetSecondaryRecommendation(It.IsAny<Quote>())).Returns(BASIC_PLAN);
            recommendationMock.Setup(q => q.GetDifferentSecondaryRecommendation(It.IsAny<Quote>())).Returns(ESSENTIAL_HEALTH);
            recommendationMock.Setup(q => q.GetPrimaryOptions(It.IsAny<Quote>())).Returns(new List<string>() { DENTAL_CARE });
            recommendationMock.Setup(q => q.GetSecondaryOptions(It.IsAny<Quote>())).Returns(new List<string>() { DENTAL_CARE });

            return new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), recommendationMock.Object);
        }

        [TestMethod]
        public async Task Test_GetPrices_2_Recommendations()
        {
            var pricingService = GetPricingService();

            var quoteWithPrices = await pricingService.GetPrices(quote);

            Assert.AreEqual(quoteWithPrices.Recommendations.Count(), 2);
        }

        [TestMethod]
        public async Task Test_GetPrices_Plans_Not_Same()
        {
            var pricingService = GetPricingService();

            var quoteWithPrices = await pricingService.GetPrices(quote);

            Assert.AreNotEqual(quoteWithPrices.Recommendations[0].PlanName, quoteWithPrices.Recommendations[1].PlanName);
        }

        [TestMethod]
        public async Task Test_GetPrices_ReplacementHealth_HasNoOptions()
        {
            var pricingService = GetPricingService();

            var quoteWithPrices = await pricingService.GetPrices(quote);

            var rhRecommendation = quoteWithPrices.Recommendations.Find(r => r.PlanType.Equals(REPLACEMENT_HEALTH));

            Assert.AreEqual(rhRecommendation?.Options.Count, 0);
        }
    }
}
