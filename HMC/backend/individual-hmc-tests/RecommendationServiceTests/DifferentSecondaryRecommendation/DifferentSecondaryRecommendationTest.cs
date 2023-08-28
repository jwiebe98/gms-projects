using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Moq;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;

namespace Gmsca.HelpMeChoose.Individual.Tests.RecommendationServiceTests.DifferentSecondaryRecommendation
{
    [TestClass]
    public class DifferentSecondaryRecommendationTest
    {
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceNotSK_Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Applicant = new()
                {
                    Province = "ON"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(OMNI_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(new Quote());

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_Returns_ExtendaPlanSKOptionOne()
        {
            Quote quote = new()
            {
                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(OMNI_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_HasTravel_Returns_ExtendaPlanSKOptionOne()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = LESS_THAN_ONE_WEEK
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(OMNI_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_HasTravel_Returns_ExtendaPlanSKOptionTwo()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(OMNI_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_HasTravelTwoToFourWeeks_Returns_ExtendaPlanSKOptionTwo()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_TO_FOUR_WEEKS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(OMNI_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION2);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_HasTravelOneOrTwoMonths_Returns_ExtendaPlanSKPlus()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_MONTHS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(OMNI_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_HasTravelTwoPlusMonths_Returns_ExtendaPlanSKPlus()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = TWO_PLUS_MONTHS
                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(OMNI_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_PLUS);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_Returns_OmniPlan()
        {

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(EXTENDA_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(new Quote());

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_HasExpendaPlanSKOptionOne_Returns_OmniPlan()
        {

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(EXTENDA_PLAN_SK_OPTION1);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(new Quote());

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_HasExpendaPlanSKOptionTwo_Returns_OmniPlan()
        {

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(EXTENDA_PLAN_SK_OPTION2);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(new Quote());

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_HasExpendaPlanSKPlus_Returns_OmniPlan()
        {

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(EXTENDA_PLAN_SK_PLUS);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(new Quote());

            Assert.AreEqual(recommendation, OMNI_PLAN);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_WithBasicPlan_Returns_ExtendaPlanSKOptionOne()
        {
            Quote quote = new()
            {
                Applicant = new()
                {
                    Province = "SK"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(BASIC_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN_SK_OPTION1);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_WithBasicPlan_Returns_ExtendaPlan()
        {
            Quote quote = new()
            {
                Applicant = new()
                {
                    Province = "ON"
                }
            };

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(BASIC_PLAN);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, EXTENDA_PLAN);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_WithPremierHealth_Returns_ChoiceHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS

                },
                Applicant = new()
                {
                    Province = "SK"
                }
            };
            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(PREMIER_HEALTH);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceNotSK_WithPremierHealth_Returns_ChoiceHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = ONE_TO_TWO_WEEKS

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(PREMIER_HEALTH);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_ProvinceSK_WithPremierHealth_Returns_PremierHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                    CoverageType = new()
                    {
                        TRAVEL
                    },
                    TravelDuration = LESS_THAN_ONE_WEEK

                },
                Applicant = new()
                {
                    Province = "AB"
                }
            };
            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(CHOICE);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, PREMIER_HEALTH);
        }
        [TestMethod]
        public void Test_DifferentSecondaryRecommendation_Returns_ChoiceHealth()
        {
            Quote quote = new()
            {
                Questions = new()
                {
                    LosingGroupBenefits = true,
                }
            };
            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns(ESSENTIAL_HEALTH);
            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), new DrugRecommendation(), new DentalRecommendation());

            var recommendation = recommendationService.GetDifferentSecondaryRecommendation(quote);

            Assert.AreEqual(recommendation, CHOICE_HEALTH);
        }



    }
}
