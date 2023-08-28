using Gmsca.Group.GA.Backend.Controllers;
using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Gmsca.HelpMeChoose.Individual.Services.Pricing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Gmsca.HelpMeChoose.Individual.Controllers
{
    public class IndividualHelpMeChooseController : APIBase
    {
        private readonly ILogger<IndividualHelpMeChooseController> _logger;

        private readonly ICosmosService _cosmosService;

        private readonly IPricingService _pricingService;

        public IndividualHelpMeChooseController(ILogger<IndividualHelpMeChooseController> logger, ICosmosService cosmosService, IPricingService pricingService)
        {
            _logger = logger;
            _cosmosService = cosmosService;
            _pricingService = pricingService;
        }

        [HttpPost("SaveQuote")]
        public async Task<IActionResult> SaveQuote(Quote quote)
        {
            _logger.LogInformation("Calling pricing API");
            var quoteWithPrices = await _pricingService.GetPrices(quote);

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
    }
}