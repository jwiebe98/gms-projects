using Gmsca.HelpMeChoose.Individual.Models.Pricing;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Extensions.Logging;
using Moq;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using static Gmsca.HelpMeChoose.Individual.Constants.Constants;
using Applicant = Gmsca.HelpMeChoose.Individual.Models.Pricing.Applicant;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class GetPriceTests
    {
        [TestMethod]
        public async Task Test_GetPrice_Response_IsNot_200()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            try
            {
                var price = await pricingService.GetPrice(SK, new List<Applicant>(), new Product());
                Assert.Fail();
            }
            catch(Exception e)
            {
                Assert.AreEqual(e.Message, "Could not get price from api");
            }
        }

        [TestMethod]
        public async Task Test_GetPrice_Response_Success()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            List<Applicant> applicants = new()
            {
                new()
                {
                    Id = "1",
                    Birthdate = DateTime.Now.AddYears(-23).ToString(ISO_8601_FORMAT)
                }
            };

            Product product = new()
            {
                Plan = OMNI_PLAN
            };
            
            var price = await pricingService.GetPrice("AB", applicants, product);

            Assert.IsTrue(price != 0);
        }
    }
}
