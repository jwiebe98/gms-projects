using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Moq;

namespace Gmsca.HelpMeChoose.Individual.Tests.PricingServiceTests
{
    [TestClass]
    public class GetEmailDescriptionTests
    {
        [TestMethod]
        public async Task Test_GetEmailDescription_Null_Returns_EmptyString()
        {
            var cosmosMock = new Mock<ICosmosService>();
            cosmosMock.Setup(q => q.GetFromDatabase(It.IsAny<string>(), It.IsAny<QueryDefinition>(), It.IsAny<Func<List<EmailDescription>, EmailDescription?>>())).Returns(Task.FromResult<EmailDescription?>(null));

            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), cosmosMock.Object, Mock.Of<IRecommendationService>());

            var emptyEmailDescription = await pricingService.GetEmailDescription("foo");

            Assert.AreEqual(emptyEmailDescription, string.Empty);
        }

        [TestMethod]
        public async Task Test_GetEmailDescription_Returns_EmailDescription()
        {
            string foo = "foo";

            var cosmosMock = new Mock<ICosmosService>();
            cosmosMock.Setup(q => q.GetFromDatabase(It.IsAny<string>(), It.IsAny<QueryDefinition>(), It.IsAny<Func<List<object>, object?>>())).Returns(Task.FromResult<object?>(new EmailDescription()
            {
                description = foo
            }));

            PricingService pricingService = new(Mock.Of<ILogger<PricingService>>(), new(), cosmosMock.Object, Mock.Of<IRecommendationService>());

            var emailDescription = await pricingService.GetEmailDescription(foo);

            Assert.AreEqual(emailDescription, foo);
        }
    }

    public class EmailDescription 
    {
        public string? description { get; set; }
    }
}
