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
    public class SecondaryConsolidatedBasePlanOmniPlanTest
    {
        [TestMethod]
        public void TestSecondaryConsolidatedBasePlan_NeedRH_NeedMentalHealthVisitsFourToEight_Returns_OmniPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        MENTAL_HEALTH_SUPPORT
                    },
                    FrequencyOfMentalHealthVisits = FOUR_TO_EIGHT

                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }
        [TestMethod]
        public void TestSecondaryConsolidatedBasePlan_NeedRH_NeedMentalHealthVisitsGreaterThanEight_Returns_OmniPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        MENTAL_HEALTH_SUPPORT
                    },
                    FrequencyOfMentalHealthVisits = GREATER_THAN_EIGHT

                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }
        [TestMethod]
        public void TestSecondaryConsolidatedBasePlan_NeedRH_NeedMentalHealthVisitsOneToThree_Returns_OmniPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        MENTAL_HEALTH_SUPPORT
                    },
                    FrequencyOfMentalHealthVisits = ONE_TO_THREE
                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }
        [TestMethod]
        public void TestSecondaryConsolidatedBasePlan_NeedRH_NeedHealthVisits_Returns_OmniPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        MENTAL_HEALTH_SUPPORT
                    },
                    HealthCarePractitionerType = new()
                    {
                        CHIROPRACTOR,MASSAGE,PHYSIOTHERAPIST
                    }
                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }

    }
}
