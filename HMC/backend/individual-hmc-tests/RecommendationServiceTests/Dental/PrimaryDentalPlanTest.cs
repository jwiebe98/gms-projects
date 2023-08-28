using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental.DentalRecommendation;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryDentalPlanTest
    {
        [TestMethod]
        public void Test_PrimaryDentalPlan_NeedsRH_NoNeedDental_Returns_Essential()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                }
            };
            var recommendation = new DentalRecommendation();
            var result = recommendation.GetPrimaryDentalPlan(quote);

            Assert.AreEqual(result, ESSENTIAL);
        }
        [TestMethod]
        public void Test_PrimaryDentalPlan_NeedRH_NeedDental_Returns_Premier()
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
            var recommendation = new DentalRecommendation();
            var result = recommendation.GetPrimaryDentalPlan(quote);

            Assert.AreEqual(result, PREMIER);
        }
        [TestMethod]
        public void Test_PrimaryDentalPlan_NoNeedRH_NoNeedDental_Returns_Basic()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                }
            };
            var recommendation = new DentalRecommendation();
            var result = recommendation.GetPrimaryDentalPlan(quote);

            Assert.AreEqual(result, BASIC);
        }
        [TestMethod]
        public void Test_PrimaryDentalPlan_NoNeedRH_NeedDental_Returns_Basic()
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
            var recommendation = new DentalRecommendation();
            var result = recommendation.GetPrimaryDentalPlan(quote);

            Assert.AreEqual(result, BASIC);
        }

    }
}