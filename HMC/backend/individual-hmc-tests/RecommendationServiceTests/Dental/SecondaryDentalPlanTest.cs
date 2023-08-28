using static Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental.DentalRecommendation;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryDentalPlanTest
    {
        [TestMethod]
        public void Test_SecondaryDentalPlan_NeedRH_NoNeedDental_Returns_Basic()
        {
            var recommendation = new DentalRecommendation();
            var result = recommendation.GetSecondaryDentalPlan();

            Assert.AreEqual(result, BASIC);
        }
    }
}
