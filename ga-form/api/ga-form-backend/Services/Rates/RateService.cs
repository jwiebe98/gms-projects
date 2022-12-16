using Gmsca.Group.GA.Backend.Models;
using Gmsca.Group.GA.Backend.Services.Cosmos;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;

namespace Gmsca.Group.GA.Backend.Services.Rates
{
    public class RateService : IRateService
    {
        private readonly ICosmosService _cosmosService;

        public RateService(ICosmosService cosmosService) => _cosmosService = cosmosService;
        public async Task<JObject> GetEffectiveRates()
        {
            Rate? latestRate = await _cosmosService.GetFromDatabase(
                "Rates",
                new QueryDefinition("SELECT * FROM c ORDER BY c._ts DESC"),
                (List<Rate> results) => results.FindAll(NotInFutureAndNotOlderThan1Year).OrderBy(rate => rate.EFFECTIVE_DATE).FirstOrDefault()
                );
            return latestRate is not null
                ? JObject.FromObject(latestRate)
                : throw new Exception(string.Format("No rates found for {0}", new DateTime().Date));
        }

        private bool NotInFutureAndNotOlderThan1Year(Rate rate)
        {
            bool isNotInFuture = rate.EFFECTIVE_DATE <= DateTime.Now.Date;
            bool isNotOlderThan1Year = rate.EFFECTIVE_DATE.AddYears(1) >= DateTime.Now.Date;
            return isNotInFuture && isNotOlderThan1Year;
        }
    }
}
