using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental.DentalRecommendation;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryDentalOptionTest
    {
        [TestMethod]
        public void Test_SecondaryDentalOption_NeedRH_NoNeedDental_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                }
            };
            var recommendation = new DentalRecommendation();
            var result = recommendation.GetSecondaryDentalOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryDentalOption_NeedRH_NeedDental_Returns_DentalCare()
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
            var result = recommendation.GetSecondaryDentalOption(quote);

            Assert.AreEqual(result, DENTAL_CARE);
        }
        [TestMethod]
        public void Test_SecondaryDentalOption_NoNeedRH_NoNeedDental_Returns_None()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = false,
                   
                }
            };
            var recommendation = new DentalRecommendation();
            var result = recommendation.GetSecondaryDentalOption(quote);

            Assert.AreEqual(result, NONE);
        }
        [TestMethod]
        public void Test_SecondaryDentalOption_NoNeedRH_NeedDental_Returns_DentalCare()
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
            var result = recommendation.GetSecondaryDentalOption(quote);

            Assert.AreEqual(result, DENTAL_CARE);
        }
    }
}
