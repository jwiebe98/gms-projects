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
    public class SecondaryConsolidatedBasePlanExtendaPlanSKOption1Test
    {
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoneedRH_MentalHealthVisitsFourToEight_ProvinceSK_Returns_ExtendaPlanSKOptionOne()
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
                    FrequencyOfMentalHealthVisits = FOUR_TO_EIGHT
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);

        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_MentalHealthVisitsGreaterThanEight_ProvinceSK_Returns_ExtendaPlanSKOptionOne()
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
                    FrequencyOfMentalHealthVisits = GREATER_THAN_EIGHT
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);

        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NeedRH_MentalHealthVisitsOneToThree_ProvinceSK_Returns_ExtendaPlanSKOptionOne()
        {

            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
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
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_NoMentalHealthVisits_ProvinceSK_Returns_ExtendaPlanSKOptionOne()
        {

            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NeedRH_NeedVision_ProvinceSK_Returns_ExtendaPlanSKOptionOne()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
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
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);
            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_NeedVision_ProvinceSK_Returns_ExtendaPlanSKOptionOne()
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
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);
            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }


    }
}
