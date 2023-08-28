using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryMentalHealthPlanTest
    {
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NeedsRH_NoNeedMentalHealth_ProvinceNotGiven_Returns_Essential()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, ESSENTIAL);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NeedsRH_NeedMentalHealth_ProvinceSK_NeedFrequencyOfVisitsOneToThree_Returns_Choice()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NeedsRH_NeedMentalHealth_ProvinceNotSK_NeedFrequencyOfVisitsOneToTwo_Returns_Choice()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, CHOICE);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NeedsRH_NeedMentalHealth_ProvinceNotGiven_NeedFrequencyOfVisitsFourToEight_Returns_Premier()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NeedsRH_NeedMentalHealth_ProvinceNotGiven_NeedFrequencyOfVisitsGreaterThanEight_Returns_Premier()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NoNeedMentalHealth_ProvinceSK_NoNeedFrequencyOfVisitsGreaterThanEight_Returns_Basic()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NoNeedMentalHealth_ProvinceNotSK_NoNeedFrequencyOfVisitsGreaterThanEight_Returns_Basic()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NeedMentalHealth_ProvinceSK_NeedFrequencyOfVisitsOneToThree_Returns_ExtendaPlanSKOption1()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NeedMentalHealth_ProvinceNotSK_NeedFrequencyOfVisitsOneToThree_Returns_ExtendaPlanSK()
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
                    Province = "ON"
                }
            };
            var recommendation = new MentalHealthRecommendation();
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NeedMentalHealth_ProvinceSK_NeedFrequencyOfVisitsFourToEight_Returns_OmniPlan()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NeedMentalHealth_ProvinceNotSK_NeedFrequencyOfVisitsFourToEight_Returns_OmniPlan()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NeedMentalHealth_ProvinceSK_NeedFrequencyOfVisitsGreaterThanEight_Returns_OmniPlan()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryMentalHealthPlan_NoNeedsRH_NeedMentalHealth_ProvinceNotSK_NeedFrequencyOfVisitsGreaterThanEight_Returns_OmniPlan()
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
            var result = recommendation.GetPrimaryMentalHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
    }
}
