using System.Net;
using Gmsca.Group.GA.Backend.Services.Cosmos;
using Gmsca.Group.GA.Backend.Services.Pricing;
using Gmsca.Group.GA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Gmsca.Group.GA.Backend.Controllers
{
    public class QuoteController : APIBase
    {
        private readonly ICosmosService _cosmosService;

        private readonly ILogger<QuoteController> _logger;

        private readonly IPricingService _pricingService;

        public QuoteController(ILogger<QuoteController> logger, ICosmosService cosmosService, IPricingService pricingService)
        {
            _logger = logger;
            _cosmosService = cosmosService;
            _pricingService = pricingService;
        }

        private async Task SetRSDInfoInQuote(Quote quote)
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", quote.regionalSalesDirectorInfo.id);

            ContactInfo? updatedContactInfo = await _cosmosService.GetFromDatabase("RegionalSalesDirectors", queryDefinition, (List<ContactInfo> results) => results.LastOrDefault());

            if (updatedContactInfo is not null)
            {
                quote.regionalSalesDirectorInfo = updatedContactInfo;
            }
        }

        [HttpPost("SaveQuote")]
        public async Task<IActionResult> SaveQuote(Quote quote)
        {
            _logger.LogInformation("Setting RSD contact info in quote");
            await SetRSDInfoInQuote(quote);

            _logger.LogInformation("Calculating prices for quote");
            Quote quoteWithPrices = await _pricingService.SetPricesInQuote(quote);

            _logger.LogInformation("Fetching database");
            Database database = _cosmosService.GetDatabase();

            _logger.LogInformation("Fetching container 'Quotes'");
            Container container = database.GetContainer("Quotes");

            _logger.LogInformation("Creating quote in Cosmos");
            ItemResponse<Quote> quoteResponse = await container.CreateItemAsync(quoteWithPrices);

            if (quoteResponse.StatusCode is not HttpStatusCode.Created)
            {
                string errorString = string.Format("When saving quote to cosmos an exception was thrown: {0}", quoteResponse.StatusCode);
                _logger.LogError(errorString);
                throw new Exception(errorString);
            }
            return Created(nameof(quoteWithPrices), quoteWithPrices);
        }

        [HttpGet("GetQuote")]
        public async Task<IActionResult> GetQuote(string quoteId)
        {
            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.quoteId = @quoteId ORDER BY c._ts ASC").WithParameter("@quoteId", quoteId);
            Quote? quote = await _cosmosService.GetFromDatabase("Quotes", queryDefinition, (List<Quote> results) => results.LastOrDefault());

            return quote is not null ? Ok(quote) : NotFound();
        }
    }
}
