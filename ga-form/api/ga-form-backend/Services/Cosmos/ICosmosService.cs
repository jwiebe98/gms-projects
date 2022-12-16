using Microsoft.Azure.Cosmos;

namespace Gmsca.Group.GA.Backend.Services.Cosmos
{
    public interface ICosmosService
    {
        Task<T?> GetFromDatabase<T>(string containerName, QueryDefinition queryDefinition, Func<List<T>, T?> action);
        Database GetDatabase();
    }
}
