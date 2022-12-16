using Gmsca.Group.GA.Backend.Configs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Gmsca.Group.GA.Backend.Services.Cosmos
{
    public class CosmosService : ICosmosService
    {
        private readonly CosmosClient _cosmosClient;

        private readonly string _dataBase;

        public CosmosService(CosmosClient cosmosClient, IOptions<CosmosDbConfig> cosmosDbConfig)
        {
            _cosmosClient = cosmosClient;
            _dataBase = cosmosDbConfig.Value.DatabaseName;
        }

        public async Task<T?> GetFromDatabase<T>(string containerName, QueryDefinition queryDefinition, Func<List<T>, T?> action)
        {
            Container container = _cosmosClient.GetContainer(_dataBase, containerName);
            FeedIterator<T> queryResultIterator = container.GetItemQueryIterator<T>(queryDefinition);
            List<FeedResponse<T>> results = new();
            while (queryResultIterator.HasMoreResults) results.Add(await queryResultIterator.ReadNextAsync());
            List<T> resultsToEvaluate = results.SelectMany(i => i).ToList();
            return action(resultsToEvaluate);
        }

        public Database GetDatabase() => _cosmosClient.GetDatabase(_dataBase);
    }
}
