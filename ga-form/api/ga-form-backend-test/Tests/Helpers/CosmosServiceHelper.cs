using Gmsca.Group.GA.Backend.Configs;
using Gmsca.Group.GA.Backend.Services.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Moq;

namespace Gmsca.Group.GA.Backend.Tests.Helpers
{
    public static class CosmosServiceHelper
    {
        public static CosmosService GetCosmosServiceThatReturnsMockedData<T>(List<T> items)
        {
            var feedResponseMock = new Mock<FeedResponse<T>>();
            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(items.GetEnumerator());

            var feedIteratorMock = new Mock<FeedIterator<T>>();
            feedIteratorMock.Setup(f => f.HasMoreResults).Returns(true);
            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));

            var containerMock = new Mock<Container>();
            containerMock
                .Setup(c => c.GetItemQueryIterator<T>(
                    It.IsAny<QueryDefinition>(),
                    It.IsAny<string>(),
                    It.IsAny<QueryRequestOptions>()))
                .Returns(feedIteratorMock.Object);

            var mockCosmosClient = new Mock<CosmosClient>();
            mockCosmosClient.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(containerMock.Object);

            var cosmosDbConfig = new CosmosDbConfig();
            cosmosDbConfig.DatabaseName = "asdf";
            var options = Options.Create(cosmosDbConfig);

            return new CosmosService(mockCosmosClient.Object, options);
        }
    }
}
