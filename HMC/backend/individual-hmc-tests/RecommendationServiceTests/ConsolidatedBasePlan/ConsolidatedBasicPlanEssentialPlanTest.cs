using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;


namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class ConsolidatedBasicPlanEssentialPlanTest
    {
        [TestMethod]
        public void Test_ConsolidatedBasicPlan_NeedsRH_NoNeedTravel_Returns_EssentialHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,

                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, ESSENTIAL_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasicPlan_NeedsRH_NoNeedDental_Returns_EssentialHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, ESSENTIAL_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasicPlan_NeedsRH_ProvinceNotGiven_Returns_EssentialHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,

                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, ESSENTIAL_HEALTH);
        }
       
    }
}
