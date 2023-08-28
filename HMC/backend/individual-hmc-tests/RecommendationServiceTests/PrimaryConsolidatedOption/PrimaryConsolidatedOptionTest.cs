using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Moq;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class PrimaryConsolidatedOptionTest
    {
        [TestMethod]
        public void ConsolidatedOptionTest_Returns_EnhancedDrug()
        {
            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetPrimaryDrugOption(It.IsAny<Quote>())).Returns(ENHANCED_DRUG);

            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetPrimaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelOption(It.IsAny<Quote>())).Returns("");
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetPrimaryOptions(new Quote());
            var expectedRecommendationList = new List<string> { "PrescriptionDrugEnhanced", "", "" };
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], ENHANCED_DRUG);
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], "");
        }
        [TestMethod]
        public void ConsolidatedOptionTest_Returns_BasicDrug()
        {
            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetPrimaryDrugOption(It.IsAny<Quote>())).Returns(BASIC_DRUG);

            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetPrimaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelOption(It.IsAny<Quote>())).Returns("");
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetPrimaryOptions(new Quote());
           
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], BASIC_DRUG);
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], "");


        }
        [TestMethod]
        public void ConsolidatedOptionTest_Returns_TravelUpto15Days()
        {
            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetPrimaryDrugOption(It.IsAny<Quote>())).Returns("");

            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetPrimaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelOption(It.IsAny<Quote>())).Returns(TRAVEL_UP_TO_15_DAYS);
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetPrimaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], TRAVEL_UP_TO_15_DAYS);
        }
        [TestMethod]
        public void ConsolidatedOptionTest_Returns_TravelUpto30Days()
        {
            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetPrimaryDrugOption(It.IsAny<Quote>())).Returns("");

            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetPrimaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelOption(It.IsAny<Quote>())).Returns(TRAVEL_UP_TO_30_DAYS);
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetPrimaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], TRAVEL_UP_TO_30_DAYS);
        }
        [TestMethod]
        public void ConsolidatedOptionTest_Returns_TravelUpto48Days()
        {
            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetPrimaryDrugOption(It.IsAny<Quote>())).Returns("");

            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetPrimaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelOption(It.IsAny<Quote>())).Returns(TRAVEL_UP_TO_48_DAYS);
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetPrimaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], TRAVEL_UP_TO_48_DAYS);
        }
        [TestMethod]
        public void ConsolidatedOptionTest_Returns_DentalCare()
        {
            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetPrimaryDrugOption(It.IsAny<Quote>())).Returns("");

            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetPrimaryDentalOption(It.IsAny<Quote>())).Returns(DENTAL_CARE);

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelOption(It.IsAny<Quote>())).Returns("");
            travelRecommendationMock.Setup(q => q.GetPrimaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetPrimaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], DENTAL_CARE);
            Assert.AreEqual(result[2], "");
        }
        [TestMethod]

        public void ConsolidatedOptionTest_Replacement_Health_Has_No_Options_WithPremierHealth()
        {
            var recommendationServiceMock = new Mock<IRecommendationService>();
            recommendationServiceMock.Setup(q => q.GetPrimaryRecommendation(It.IsAny<Quote>())).Returns(PREMIER_HEALTH);

            var result = recommendationServiceMock.Object.GetPrimaryOptions(new Quote());
            Assert.IsTrue(result == null);
        }
        [TestMethod]
        public void ConsolidatedOptionTest_Replacement_Health_Has_No_Options_WithEssentialHealth()
        {
            var recommendationServiceMock = new Mock<IRecommendationService>();
            recommendationServiceMock.Setup(q => q.GetPrimaryRecommendation(It.IsAny<Quote>())).Returns(ESSENTIAL_HEALTH);

            var result = recommendationServiceMock.Object.GetPrimaryOptions(new Quote());
            Assert.IsTrue(result == null);
        }
        [TestMethod]
        public void ConsolidatedOptionTest_Replacement_Health_Has_No_Options_WithChoiceHealth()
        {
            var recommendationServiceMock = new Mock<IRecommendationService>();
            recommendationServiceMock.Setup(q => q.GetPrimaryRecommendation(It.IsAny<Quote>())).Returns(CHOICE_HEALTH);

            var result = recommendationServiceMock.Object.GetPrimaryOptions(new Quote());
            Assert.IsTrue(result == null);
        }
    }
}
