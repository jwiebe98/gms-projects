using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Extensions.Logging;
using Moq;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using static Gmsca.HelpMeChoose.Individual.Constants.Constants;


namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class GetApplicantsTests
    {
        [TestMethod]
        public void Test_GetApplicants_YOU()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var applicants = pricingService.GetApplicants(new() { 
                Applicant = new() { 
                    ApplicantAge = 23
                },
                Questions = new() { 
                    NumberPeopleCovered = YOU
                }
            });

            Assert.AreEqual(applicants.Count(), 1);
            Assert.AreEqual(applicants[0].Id, "1");
            Assert.AreEqual(applicants[0].Birthdate, DateTime.UtcNow.AddYears(-23).ToString(ISO_8601_FORMAT));
        }

        [TestMethod]
        public void Test_GetApplicants_SPOUSE()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var applicants = pricingService.GetApplicants(new()
            {
                Applicant = new()
                {
                    SpouseAge = 23
                },
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_SPOUSE
                }
            });

            Assert.AreEqual(applicants.Count(), 2);
            Assert.AreEqual(applicants[1].Id, "2");
            Assert.AreEqual(applicants[1].Birthdate, DateTime.UtcNow.AddYears(-23).ToString(ISO_8601_FORMAT));
        }

        [TestMethod]
        public void Test_GetApplicants_CHILD()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var applicants = pricingService.GetApplicants(new()
            {
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILD
                }
            });

            Assert.AreEqual(applicants.Count(), 2);
            Assert.AreEqual(applicants[1].Id, "3");
            Assert.AreEqual(applicants[1].Birthdate, DateTime.UtcNow.AddYears(-5).ToString(ISO_8601_FORMAT));
        }

        [TestMethod]
        public void Test_GetApplicants_CHILDREN()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            var applicants = pricingService.GetApplicants(new()
            {
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILDREN
                }
            });

            Assert.AreEqual(applicants.Count(), 3);
            Assert.AreEqual(applicants[2].Id, "4");
            Assert.AreEqual(applicants[2].Birthdate, DateTime.UtcNow.AddYears(-5).ToString(ISO_8601_FORMAT));
        }
    }
}
