using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryMentalHealthPlanTest
    {
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NeedsRH_NoNeedHealth_ProvinceNotGiven_Returns_Basic()
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
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NeedsRH_NoNeedHealth_ProvinceSK_NeedFrequencyOfVisitsOneToThree_Returns_ExtendaPlanSKOption1()
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
                    FrequencyOfMentalHealthVisits = ONE_TO_THREE

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NeedsRH_NoNeedHealth_ProvinceNotSK_NeedFrequencyOfVisitsOneToThree_Returns_ExtendaPlan()
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
                    FrequencyOfMentalHealthVisits = ONE_TO_THREE

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NeedsRH_NeedHealth_ProvinceNotGiven_NeedFrequencyOfVisitsFourToEight_Returns_OmniPlan()
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
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);
            Assert.AreEqual(OMNI_PLAN, result);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NeedsRH_NoNeedHealth_ProvinceNotGiven_NeedFrequencyOfVisitsGreaterThanEight_Returns_OmniPlan()
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
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NoNeedHealth_ProvinceNotGiven_NeedFrequencyOfVisitsGreaterThanEight_Returns_ExtendaPlanSKOption1()
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
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NoNeedHealth_ProvinceNotSK_NeedFrequencyOfVisitsGreaterThanEight_Returns_ExtendaPlan()
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
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NeedHealth_ProvinceSK_NeedFrequencyOfVisitsOneToThree_Returns_OmniPlan()
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
                    Province = "SK"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NeedHealth_ProvinceNotSK_NeedFrequencyOfVisitsOneToThree_Returns_OmniPlan()
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
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NeedHealth_ProvinceNotSK_NeedFrequencyOfVisitsFourToEight_Returns_ExtendaPlanSKOption1()
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
                    FrequencyOfMentalHealthVisits = FOUR_TO_EIGHT
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NeedHealth_ProvinceNotSK_NeedFrequencyOfVisitsFourToEight_Returns_ExtendaPlan()
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
                    FrequencyOfMentalHealthVisits = FOUR_TO_EIGHT
                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NeedHealth_ProvinceNotSK_NeedFrequencyOfVisitsGreaterThanEight_Returns_ExtendaPlanSKOption1()
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
                    FrequencyOfMentalHealthVisits = GREATER_THAN_EIGHT
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryMentalHealthPlan_NoNeedsRH_NeedHealth_ProvinceNotSK_NeedFrequencyOfVisitsGreaterThanEight_Returns_ExtendaPlan()
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
                    FrequencyOfMentalHealthVisits = GREATER_THAN_EIGHT
                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetSecondaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
    }
}
