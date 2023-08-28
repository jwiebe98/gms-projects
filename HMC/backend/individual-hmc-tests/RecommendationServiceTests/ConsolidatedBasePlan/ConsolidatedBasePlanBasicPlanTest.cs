using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class ConsolidatedBasePlanBasicPlanTest
    {
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NoNeedTravel_ProvinceNotGiven_NoNeedTravelDurationTwoPlusMonths_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };

            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NoNeedsPrescriptionDrugs_NotHasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());             
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsRarelyorNever_NotHasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
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

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }

        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsRarelyorNever_HasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
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

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_NotHasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
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

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsOneOrTwo_HasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
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

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_NotHasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_OR_MORE,
                    ExistingPrescription = false
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());             
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedsRH_NeedsPrescriptionDrugs_HasPrescriptionDrugsThreeOrMore_HasExistingPrescription_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        PRESCRIPTION_MEDICATION
                    },
                    NumberOfDrugPrescriptions = THREE_OR_MORE,
                    ExistingPrescription = true
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());             
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedRH_NoNeedDental_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());             
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedRH_NeedDental_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        DENTAL
                    }
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());             
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedRH_NoNeedHealth_ProvinceSK__NoNeedHealthCarePractitioners_Returns_Basic()
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
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
        [TestMethod]
        public void Test_ConsolidatedBasePlan_NoNeedRH_NoNeedHealth_ProvinceNotSK__NoNeedHealthCarePractitioners_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());             
            
            var recommendation = recommendationService.GetPrimaryRecommendation(quote);

            Assert.AreEqual(recommendation, BASIC_PLAN);
        }
    }
}
