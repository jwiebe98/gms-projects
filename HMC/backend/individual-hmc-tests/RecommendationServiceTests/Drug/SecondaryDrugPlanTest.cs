using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug.DrugRecommendation;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryDrugPlanTest
    {
        [TestMethod]
        public void Test_SecondaryDrugPlan_Returns_Basic()
        {

            var recommendation = new DrugRecommendation();
            var result = recommendation.GetSecondaryDrugPlan();

            Assert.AreEqual(result, BASIC);
        }
    }
}
