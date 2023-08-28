using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryHealthPlanTest
    {
        [TestMethod]
        public void Test_PrimaryHealthPlan_NeedsRH_NoNeedHealth_ProvinceNotGiven_Returns_Essential()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);
            Assert.AreEqual(result, ESSENTIAL);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NeedsRH_NeedHealth_ProvinceSK__Returns_Essential()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, ESSENTIAL);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NeedsRH_NeedHealth_ProvinceNotSK__Returns_Essential()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, ESSENTIAL);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NeedsRH_NeedHealth_ProvinceNotGiven__NeedHealthCarePractitioners_Returns_Premier()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NoNeedRH_NoNeedHealth_ProvinceSK__NoNeedHealthCarePractitioners_Returns_Basic()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NoNeedRH_NoNeedHealth_ProvinceNotSK__NoNeedHealthCarePractitioners_Returns_Basic()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NoNeedRH_NeedHealth_ProvinceSK__NoNeedHealthCarePractitioners_Returns_ExtendaPlanSkOption1()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NoNeedRH_NeedHealth_ProvinceNotSK__NoNeedHealthCarePractitioners_Returns_ExtendaPlan()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NoNeedRH_NeedHealth_ProvinceSK__NeedHealthCarePractitioners_Returns_OmniPlan()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_PrimaryHealthPlan_NoNeedRH_NeedHealth_ProvinceNotSK__NeedHealthCarePractitioners_Returns_OmniPlan()
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
            var result = recommendation.GetPrimaryHealthPlan(quote);

            Assert.AreEqual(result, OMNI_PLAN);
        }
    }
}
