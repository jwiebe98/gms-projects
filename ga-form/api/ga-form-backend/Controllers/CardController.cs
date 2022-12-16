using Gmsca.Group.GA.Backend.Constants;
using Gmsca.Group.GA.Backend.Models;
using Gmsca.Group.GA.Backend.Services.Rates;
using Gmsca.Group.GA.Models.Validation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Gmsca.Group.GA.Backend.Controllers
{
    public class CardController : APIBase
    {
        private readonly ILogger<CardController> _logger;

        private readonly JToken _rates;

        public CardController(ILogger<CardController> logger, IRateService rateService)
        {
            _logger = logger;
            _rates = rateService.GetEffectiveRates().Result;
        }

        [HttpGet("GetCardPrices")]
        public IActionResult GetCardPrices(
            [StringRange(AllowableValues = new[] { "BC", "AB", "SK", "MB", "ON", "NS", "PE", "NL", "YK", "NT" })]
            string province,
            [StringRange(AllowableValues = new[] { "_500", "_1000", "_1500", "_2000" })]
            string dentalSilverCombinedYearlyMaximum,
            [StringRange(AllowableValues = new[] { "_500", "_1000", "_1500", "_2000" })]
            string dentalGoldCombinedYearlyMaximum,
            [StringRange(AllowableValues = new[] { "_500", "_1000", "_1500", "_2000" })]
            string dentalPlatinumCombinedYearlyMaximum)
        {
            _logger.LogInformation("Creating new cards component");
            Cards cards = new();

            _logger.LogInformation("Getting health rates");
            cards.health = _rates[CoverageType.provinceRates][province][CoverageType.health].ToObject<CardTiers>();

            _logger.LogInformation("Getting dental rates");
            JToken dentalRates = _rates[CoverageType.provinceRates][province][CoverageType.dental];

            cards.dental.silver = dentalRates[dentalSilverCombinedYearlyMaximum][CoverageTier.silver].ToObject<CardTypes>();
            cards.dental.gold = dentalRates[dentalGoldCombinedYearlyMaximum][CoverageTier.gold].ToObject<CardTypes>();
            cards.dental.platinum = dentalRates[dentalPlatinumCombinedYearlyMaximum][CoverageTier.platinum].ToObject<CardTypes>();

            _logger.LogInformation("Returning cards");
            return Ok(cards);
        }
    }
}
