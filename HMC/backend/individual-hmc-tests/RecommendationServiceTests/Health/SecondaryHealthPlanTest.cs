using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryHealthPlanTest
    {
        [TestMethod]
        public void Test_SecondaryHealthPlan_NeedsRH_NoNeedHealth_ProvinceNotGiven_Returns_Basic()
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
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NeedsRH_NeedHealth_ProvinceSK__Returns_ExtendaPlanSKOptionOne()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        HEALTH_PRACTITIONERS
                    }
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NeedsRH_NeedHealth_ProvinceNotSK__Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        HEALTH_PRACTITIONERS
                    }
                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NeedsRH_NeedHealth_ProvinceNotGiven__Returns_OmniPlan()
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
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NotNeedsRH_NotNeedHealth_ProvinceSK__Returns_ExtendaPlanSKOptionOne()
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
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NotNeedsRH_NotNeedHealth_ProvinceNotSK__Returns_ExtendaPlan()
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
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NotNeedsRH_NeedHealth_ProvinceSK__Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        HEALTH_PRACTITIONERS
                    }
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NotNeedsRH_NeedHealth_ProvinceNotSK__Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                    CoverageType = new()
                    {
                        HEALTH_PRACTITIONERS
                    }
                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NotNeedsRH_NeedHealth_ProvinceSK__Returns_ExtendaPlanSKOptionOne()
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
                    Province = "SK"
                }
            };
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_SecondaryHealthPlan_NotNeedsRH_NeedHealth_ProvinceNotSK__Returns_ExtendaPlan()
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
            var recommendation = new HealthRecommendation();
            var result = recommendation.GetSecondaryHealthPlan(quote);
            Assert.AreEqual(result, EXTENDA_PLAN);
        }
    }
}
