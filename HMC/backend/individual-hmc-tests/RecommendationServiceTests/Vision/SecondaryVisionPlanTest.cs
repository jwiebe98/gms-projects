using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;


namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryVisionPlanTest
    {
        [TestMethod]
        public void Test_SecondaryVision_NeedsVision_ProvinceAny_Returns_Basic()
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
            var recommendation = new VisionRecommendation();
            var result = recommendation.GetSecondaryVisionPlan(quote);
            Assert.AreEqual(result, BASIC);

        }
        [TestMethod]
        public void Test_SecondaryVision_NeedsRH_NeedsVision_ProvinceAny_Returns_Extenda_Plan()
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
            var result = recommendation.GetSecondaryVisionPlan(quote);
            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryVision_NeedsRH_NeedsVision_ProvinceSK_Returns_Extenda_Plan_SK_Option1()
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
            var result = recommendation.GetSecondaryVisionPlan(quote);
            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }

        [TestMethod]
        public void Test_SecondaryVision_NoNeedsRH_NoNeedsVision_ProvinceAny_Returns_Basic()
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
            var result = recommendation.GetSecondaryVisionPlan(quote);
            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_SecondaryVision_NoNeedsRH_NeedsVision_ProvinceNotSK_Returns_Extenda_Plan()
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
            var result = recommendation.GetSecondaryVisionPlan(quote);
            Assert.AreEqual(result, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_SecondaryVision_NoNeedsRH_NeedsVision_ProvinceSK_Returns_Extenda_Plan_SK_Option1()
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
            var result = recommendation.GetSecondaryVisionPlan(quote);
            Assert.AreEqual(result, EXTENDA_PLAN_SK_OPTION1);
        }
    }
}
