using Gmsca.Group.GA.Backend.Configs;
using Gmsca.Group.GA.Backend.Models;
using Gmsca.Group.GA.Backend.Services.Cosmos;
using Gmsca.Group.GA.Backend.Services.Rates;
using Gmsca.Group.GA.Backend.Tests.Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Moq;

namespace Gmsca.Group.GA.Backend.Tests.Unit.Services.CosmosServiceTests
{
    [TestClass]
    public class CosmoServiceTests
    {
        [TestMethod]
        public void CosmosService_AssertGetDatabaseReturnsDatabase()
        {
            var mockDatabase = new Mock<Database>();
            mockDatabase.Setup(x => x.ToString()).Returns("This is the mocked database");

            var mockCosmosClient = new Mock<CosmosClient>();
            mockCosmosClient.Setup(x => x.GetDatabase(It.IsAny<string>())).Returns(mockDatabase.Object);

            var cosmosDbConfig = new CosmosDbConfig();
            cosmosDbConfig.DatabaseName = "asdf";
            var options = Options.Create(cosmosDbConfig);

            var cosmosService = new CosmosService(mockCosmosClient.Object, options);

            var database = cosmosService.GetDatabase();

            Assert.AreEqual(mockDatabase.Object.ToString(), database.ToString());
        }

        [TestMethod]
        public async Task CosmosService_AssertGetFromDatabaseUsesProvidedAction()
        {
            List<int> nums = new() { 1, 2, 3, 4, 5 };
            var cosmosService = CosmosServiceHelper.GetCosmosServiceThatReturnsMockedData(nums);

            var selectedNum = await cosmosService.GetFromDatabase<int>("asdf", new QueryDefinition("asdf"), nums => nums.LastOrDefault());

            Assert.AreEqual(selectedNum, nums.LastOrDefault());
        }


    }
}
