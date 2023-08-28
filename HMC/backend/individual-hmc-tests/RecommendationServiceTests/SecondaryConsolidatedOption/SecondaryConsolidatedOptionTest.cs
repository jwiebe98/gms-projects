using System;
using System.Collections.Generic;
using System.Linq;
using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Moq;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;
using Gmsca.HelpMeChoose.Individual.Interfaces;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class SecondaryConsolidatedOptionTest
    {
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Returns_EnhancedDrug()
        {
            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetSecondaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelOption(It.IsAny<Quote>())).Returns("");
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetSecondaryDrugOption(It.IsAny<Quote>())).Returns(ENHANCED_DRUG);

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetSecondaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0],"");
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], ENHANCED_DRUG);
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Returns_BasicDrug()
        {
            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetSecondaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelOption(It.IsAny<Quote>())).Returns("");
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetSecondaryDrugOption(It.IsAny<Quote>())).Returns(BASIC_DRUG);

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetSecondaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], BASIC_DRUG);
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Returns_TravelUpto15Days()
        {
            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetSecondaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelOption(It.IsAny<Quote>())).Returns(TRAVEL_UP_TO_15_DAYS);
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetSecondaryDrugOption(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetSecondaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], TRAVEL_UP_TO_15_DAYS);
            Assert.AreEqual(result[2],"");
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Returns_TravelUpto30Days()
        {
            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetSecondaryDentalOption(It.IsAny<Quote>())).Returns("");

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelOption(It.IsAny<Quote>())).Returns(TRAVEL_UP_TO_30_DAYS);
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetSecondaryDrugOption(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetSecondaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], TRAVEL_UP_TO_30_DAYS);
            Assert.AreEqual(result[2], "");
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Returns_TravelUpto48Days()
        {
            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetSecondaryDentalOption(It.IsAny<Quote>())).Returns("");
            
            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelOption(It.IsAny<Quote>())).Returns(TRAVEL_UP_TO_48_DAYS);
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetSecondaryDrugOption(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetSecondaryOptions(new Quote());
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "");
            Assert.AreEqual(result[1], TRAVEL_UP_TO_48_DAYS);
            Assert.AreEqual(result[2], "");
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Returns_DentalCare()
        {
            var dentalRecommendationMock = new Mock<IDentalRecommendation>();
            dentalRecommendationMock.Setup(q => q.GetSecondaryDentalOption(It.IsAny<Quote>())).Returns(DENTAL_CARE);

            var travelRecommendationMock = new Mock<ITravelRecommendation>();
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelOption(It.IsAny<Quote>())).Returns("");
            travelRecommendationMock.Setup(q => q.GetSecondaryTravelPlan(It.IsAny<Quote>())).Returns("");

            var drugRecommendationMock = new Mock<IDrugRecommendation>();
            drugRecommendationMock.Setup(q => q.GetSecondaryDrugOption(It.IsAny<Quote>())).Returns("");

            var recommendationService = new RecommendationService(new VisionRecommendation(), travelRecommendationMock.Object, new HealthRecommendation(), new MentalHealthRecommendation(), drugRecommendationMock.Object, dentalRecommendationMock.Object);

            var result = recommendationService.GetSecondaryOptions(new Quote());
            var expectedRecommendationList = new List<string> { "DENTAL_CARE", "", "" };
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], DENTAL_CARE);
            Assert.AreEqual(result[1], "");
            Assert.AreEqual(result[2], "");
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Replacement_Health_Has_No_Options_WithPremierHealth()
        {
            var recommendationServiceMock = new Mock<IRecommendationService>();
            recommendationServiceMock.Setup(q => q.GetSecondaryRecommendation(It.IsAny<Quote>())).Returns(PREMIER_HEALTH);

            var result = recommendationServiceMock.Object.GetSecondaryOptions(new Quote());
            Assert.IsTrue(result == null);
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Replacement_Health_Has_No_Options_WithEssentialHealth()
        {
            var recommendationServiceMock = new Mock<IRecommendationService>();
            recommendationServiceMock.Setup(q => q.GetSecondaryRecommendation(It.IsAny<Quote>())).Returns(ESSENTIAL_HEALTH);

            var result = recommendationServiceMock.Object.GetSecondaryOptions(new Quote());
            Assert.IsTrue(result == null);
        }
        [TestMethod]
        public void SecondaryConsolidatedOptionTest_Replacement_Health_Has_No_Options_WithChoiceHealth()
        {
            var recommendationServiceMock = new Mock<IRecommendationService>();
            recommendationServiceMock.Setup(q => q.GetSecondaryRecommendation(It.IsAny<Quote>())).Returns(CHOICE_HEALTH);

            var result = recommendationServiceMock.Object.GetSecondaryOptions(new Quote());
            Assert.IsTrue(result == null);
        }
    }
}
