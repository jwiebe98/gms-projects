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
    public class SecondaryConsolidatedBasePlanExtendaPlanSKOption2Test
    {
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_withTravelLessThanOneWeek_Returns_ExtendaPlusSKOptionTwo()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = LESS_THAN_ONE_WEEK
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_withTravel_WithOneToTwoWeeks_ReturnsExtendaPlusSKOptionTwo()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_withTravel_WithTwoToFourWeeks_Returns_ExtendaPlusSKOptionTwo()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_TO_FOUR_WEEKS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }

    }
}
