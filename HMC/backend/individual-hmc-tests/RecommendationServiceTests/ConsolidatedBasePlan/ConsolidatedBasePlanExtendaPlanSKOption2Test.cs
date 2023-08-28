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
    public class ConsolidatedBasePlanExtendaPlanSKOption2Test
    {
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoToFourWeeks_Returns_ExtendaPlanSKOption2()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
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
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationlessThanOneWeeks_Returns_ExtendaPlanSKOption2()
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
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationTwoToFourWeeks_Returns_ExtendaPlanSKOption2()
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
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoWeeks_Returns_ExtendaPlanSKOption2()
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
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
    }
}
