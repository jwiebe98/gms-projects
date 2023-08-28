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
    public class ConsolodatedBasePlanPremierPlanTest
    {
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationOneToTwoWeek_Returns_Premier()
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
                    TravelDuration = ONE_TO_TWO_WEEKS

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationOneToTwoWeek_Returns_PremierHealth()
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
                    TravelDuration = ONE_TO_TWO_WEEKS

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedPrescriptionDrugsThreeOrMore__Returns_PremierHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_PLUS,
                    ExistingPrescription = false
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);
            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedPrescriptionDrugsThreeOrMore_HasExistingPrescriptionReturns_PremierHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_PLUS,
                    ExistingPrescription = true
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedRH_NeedDental_Returns_PremierHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        DENTAL
                    }
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedHealth_ProvinceNotGiven__NeedHealthCarePractitioners_Returns_PremierHealth()
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
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedMentalHealthVisitsFourToEight_ProvinceNotGiven_Returns_PremierHealth()
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
                    FrequencyOfMentalHealthVisits = FOUR_TO_EIGHT
                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
       
    }
}
