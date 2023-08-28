using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;


namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryVisionPlanTest
    {
        [TestMethod]
        public void Test_PrimaryVision_NeedsRH_NoVision_AnyProvince_Returns_Essential()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true
                },
                Applicant = new()
                {
                    Province = "foo"
                }
            };

            var recommendation = new VisionRecommendation();
            var result = recommendation.GetPrimaryVisionPlan(quote);

            Assert.AreEqual(result, ESSENTIAL);

        }

        [TestMethod]
        public void Test_PrimaryVision_NotNeedsRH_Vision_AnyProvince_Returns_Basic()
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

            var recommendation = new VisionRecommendation();
            var result = recommendation.GetPrimaryVisionPlan(quote);
            Assert.AreEqual(result, BASIC);
        }



        [TestMethod]

        public void Test_PrimaryVision_NotNeeedsRH_Vision_ProvinceNotSK_Returns_ExtendaPlan()
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
            var recommendation = new VisionRecommendation();
            var result = recommendation.GetPrimaryVisionPlan(quote);
            Assert.AreEqual(result, EXTENDA_PLAN);
        }

        [TestMethod]

        public void Test_PrimaryVision_NeeedsRH_Vision_ProvinceNotSK_Returns_Choice()
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

            var recommendation = new VisionRecommendation();
            var result = recommendation.GetPrimaryVisionPlan(quote);
            Assert.AreEqual(result, CHOICE);


        }
        [TestMethod]
        public void Test_PrimaryVision_NeeedsRH_Vision_ProvinceSK_Returns_Choice()
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

            var recommendation = new VisionRecommendation();
            var result = recommendation.GetPrimaryVisionPlan(quote);
            Assert.AreEqual(result, CHOICE);

        }
        [TestMethod]
        public void Test_PrimaryVision_NotNeeedsRH_Vision_ProvinceSK_Returns_Extenda_Plan_SK_Option1()
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

            var recommendation = new VisionRecommendation();
            var result = recommendation.GetPrimaryVisionPlan(quote);
            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
    }
}
