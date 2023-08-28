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
    public class SecondaryConsolidatedBasePlanExtendaPlanTest
    {
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_TravelLessThanOneWeek_ProvinceNotSK_Returns_ExtendaPlan()
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
                    Province = "ON"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_TravelOneToTwoWeek_ProvinceNotSK_Returns_ExtendaPlan()
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
                    Province = "ON"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_TravelTwoToFourWeek_ProvinceNotSK_Returns_ExtendaPlan()
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
                    Province = "ON"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_TravelOneToTwoMonths_ProvinceNotSK_Returns_ExtendaPlan()
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
                    TravelDuration = ONE_TO_TWO_MONTHS
                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_TravelTwoPlusMonths_ProvinceNotSK_Returns_ExtendaPlan()
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
                    TravelDuration = TWO_PLUS_MONTHS
                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NeedRH_NeedVision_ProvinceNotSK_Returns_ExtendaPlan()
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
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);
            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_NeedVision_ProvinceNotSK_Returns_ExtendaPlan()
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
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);
            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_NoNeedHealth_ProvinceNotSK_Returns_ExtendaPlan()
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
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_NeedHealth_NeedHealthCarePractitioner_ProvinceNotSK_Returns_ExtendaPlan()
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
                    HealthCarePractitionerType = new()
                    {
                        CHIROPRACTOR,MASSAGE,PHYSIOTHERAPIST
                    }
                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);

        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NeedRH_NeedHealth_NeedHealthCarePractitioner_ProvinceNotSK_Returns_ExtendaPlan()
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

                },
                Applicant = new()
                {
                    Province = "ON"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);

        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NeedRH_MentalHealthVisitsOneToThree_ProvinceNotSK_Returns_ExtendaPlan()
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
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);

        }

        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_MentalHealthVisitsFourToEight_ProvinceNotSK_Returns_ExtendaPlan()
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
                    Province = "AB"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);

        }
        [TestMethod]
        public void Test_SecondaryConsolidatedBasePlan_NoNeedRH_NeedMentalHealthVisitsGreaterThanEight_ProvinceNotSK_Returns_ExtendaPlan()
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
                    Province = "ON"
                }
            };
            var recommendationService = new RecommendationService(new VisionRecommendation(), new TravelRecommendation(), new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());
            var recommendation = recommendationService.GetSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);

        }

    }
}
