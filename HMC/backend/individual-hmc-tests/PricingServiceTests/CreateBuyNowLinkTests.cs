using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Extensions.Logging;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Moq;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Models.BuyNowPayload;
using Gmsca.HelpMeChoose.Individual.Models;
using Newtonsoft.Json;
using System.Text;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class CreateBuyNowLinkTests
    {
        [TestMethod]
        public void Test_CreateBuyNowLink_PlanAdded()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string buyNowLink = pricingService.CreateBuyNowLink(OMNI_PLAN, new()
            {
                DENTAL_CARE
            },
            new()
            {
                Applicant = new()
                {
                    Province = SK
                },
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILD
                }
            });

            string bytes = buyNowLink.Split("settings=")[1];

            var buyNowPayload = DecodeByteStringToBuyNowPayload(bytes);

            Assert.IsTrue(buyNowPayload.pls.Contains(3));
        }

        [TestMethod]
        public void Test_CreateBuyNowLink_AddsOptions()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string buyNowLink = pricingService.CreateBuyNowLink(CHOICE_HEALTH, new() { DENTAL_CARE, BASIC_DRUG }, new() { 
                Questions = new()
                {
                    NumberPeopleCovered = YOU
                }
            });

            string bytes = buyNowLink.Split("settings=")[1];

            var buyNowPayload = DecodeByteStringToBuyNowPayload(bytes);

            Assert.IsTrue(buyNowPayload.pls.Contains(4));
            Assert.IsTrue(buyNowPayload.pls.Contains(5));
        }

        [TestMethod]
        public void Test_CreateBuyNowLink_AnyPlanThatContainsExtendaMapsToExtendaPlan()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string buyNowLink = pricingService.CreateBuyNowLink("fooExtendabar", new(),
            new()
            {
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILD
                }
            });

            string bytes = buyNowLink.Split("settings=")[1];

            var buyNowPayload = DecodeByteStringToBuyNowPayload(bytes);

            Assert.IsTrue(buyNowPayload.pls.Contains(2));
        }

        [TestMethod]
        public void Test_CreateBuyNowLink_Province()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string buyNowLink = pricingService.CreateBuyNowLink(OMNI_PLAN, new(),
            new()
            {
                Applicant = new()
                {
                    Province = SK
                },
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILD
                }
            });

            string bytes = buyNowLink.Split("settings=")[1];

            var buyNowPayload = DecodeByteStringToBuyNowPayload(bytes);

            Assert.AreEqual(buyNowPayload.pr, SK);
        }

        [TestMethod]
        public void Test_CreateBuyNowLink_NumberOfPeopleCovered()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string buyNowLink = pricingService.CreateBuyNowLink(OMNI_PLAN, new(),
            new()
            {
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILD
                }
            });

            string bytes = buyNowLink.Split("settings=")[1];

            var buyNowPayload = DecodeByteStringToBuyNowPayload(bytes);

            Assert.AreEqual(buyNowPayload.nod, 2);
        }

        [TestMethod]
        public void Test_CreateBuyNowLink_ApplicantAge_Largest()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string buyNowLink = pricingService.CreateBuyNowLink(OMNI_PLAN, new(),
            new()
            {
                Applicant = new()
                {
                    ApplicantAge = 22,
                    SpouseAge = 23
                },
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILD
                }
            });

            string bytes = buyNowLink.Split("settings=")[1];

            var buyNowPayload = DecodeByteStringToBuyNowPayload(bytes);

            Assert.AreEqual(buyNowPayload.aooa, 23);
        }

        [TestMethod]
        public void Test_CreateBuyNowLink_LosingGroupBenefits()
        {
            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), Mock.Of<ICosmosService>(), Mock.Of<IRecommendationService>());

            string buyNowLink = pricingService.CreateBuyNowLink(OMNI_PLAN, new(),
            new()
            {
                Questions = new()
                {
                    NumberPeopleCovered = YOU_YOUR_CHILD,
                    LosingGroupBenefits = true
                }
            });

            string bytes = buyNowLink.Split("settings=")[1];

            var buyNowPayload = DecodeByteStringToBuyNowPayload(bytes);

            Assert.AreEqual(buyNowPayload.lep, true);
        }

        private BuyNowPayload DecodeByteStringToBuyNowPayload(string byteString)
        {
            byte[] byteArray = Convert.FromBase64String(byteString);
            string jsonBack = Encoding.UTF8.GetString(byteArray);
            var buyNowPayload =  JsonConvert.DeserializeObject<BuyNowPayload>(jsonBack);
            return buyNowPayload is not null ? buyNowPayload : throw new Exception("Could not decode buy now payload string!");
        }
    }
}
