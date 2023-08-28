using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class ConsolidatedBasePlanChoiceTest
    {
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedTravel_ProvinceNotSK_NeedTravelDurationLessThanOneWeek_Returns_ChoiceHealth()
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
                    TravelDuration = LESS_THAN_ONE_WEEK

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedTravel_ProvinceSK_NeedTravelDurationLessThanOneWeek_Returns_ChoiceHealth()
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
                    TravelDuration = LESS_THAN_ONE_WEEK

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_NotHasExistingPresription_HasRarelyOrNeverDrugPresriptionReturns_ChoiceHealth()
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
                    NumberOfDrugPrescriptions = RARELY_OR_NEVER,
                    ExistingPrescription = false
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_HasExistingPresription_HasRarelyOrNeverDrugPresriptionReturns_ChoiceHealth()
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
                    NumberOfDrugPrescriptions = RARELY_OR_NEVER,
                    ExistingPrescription = true
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_HasNotExistingPresription_HasOneOrTwoDrugPresriptionReturns_ChoiceHealth()
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
                    NumberOfDrugPrescriptions = ONE_OR_TWO,
                    ExistingPrescription = false
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_PrimaryDrugPlan_NeedsRH_NeedsPrescriptionDrugs_HasExistingPresription_HasOneOrTwoDrugPresriptionReturns_ChoiceHealth()
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
                    NumberOfDrugPrescriptions = ONE_OR_TWO,
                    ExistingPrescription = true
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeeedsRH_Vision_ProvinceNotSK_Returns_ChoiceHealth()
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
                    Province = "AB"
                }
            };

            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeeedsRH_Vision_ProvinceSK_Returns_ChoiceHealth()
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
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);
            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedMentalHealth_ProvinceSK_NeedFrequencyOfVisitsOneToThree_Returns_ChoiceHealth()
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
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(),new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NeedsRH_NeedMentalHealth_ProvinceNotSK_NeedFrequencyOfVisitsOneToThree_Returns_ChoiceHealth()
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
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
    }
}
