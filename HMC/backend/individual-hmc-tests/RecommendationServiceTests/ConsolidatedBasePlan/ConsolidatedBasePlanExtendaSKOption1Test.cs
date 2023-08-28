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
    public class ConsolidatedBasePlanExtendaSKOption1Test
    {
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NeedMentalHealth_ProvinceSK_NeedFrequencyOfVisitsOneToThree_Returns_ExtendaPlanSKOption1()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        HEALTH_PRACTITIONERS
                    },
                    FrequencyOfMentalHealthVisits = ONE_TO_THREE

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeeedsRH_Vision_ProvinceSK_Returns_Extenda_Plan_SK_Option1()
        {

            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        VISION
                    }
                },

                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);
            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
    }
}
