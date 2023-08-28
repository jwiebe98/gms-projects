using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Models.Pricing;
using static Gmsca.HelpMeChoose.Individual.Constants.Constants;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;
using Applicant = Gmsca.HelpMeChoose.Individual.Models.Pricing.Applicant;
using Newtonsoft.Json;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation;
using Gmsca.HelpMeChoose.Individual.Models.BuyNowPayload;
using Gmsca.HelpMeChoose.Individual.Services.Cosmos;
using Microsoft.Azure.Cosmos;
using Azure;
using System.Net;

namespace Gmsca.HelpMeChoose.Individual.Services.Pricing
{
    public class PricingService : IPricingService
    {
        ILogger<PricingService> _logger;
        HttpClient _httpClient;
        IRecommendationService _recommendationService;
        private readonly ICosmosService _cosmosService;

        public PricingService(ILogger<PricingService> logger, HttpClient httpClient, ICosmosService cosmosService, IRecommendationService recommendationService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _recommendationService = recommendationService;
            _cosmosService = cosmosService;
        }

        public async Task<Quote> GetPrices(Quote quote)
        {
            var primaryRecommendation = _recommendationService.GetPrimaryRecommendation(quote);
            var secondaryRecommendation = _recommendationService.GetSecondaryRecommendation(quote);

            var finalSecondaryRecommendation = secondaryRecommendation.Equals(primaryRecommendation) ? _recommendationService.GetDifferentSecondaryRecommendation(quote) : secondaryRecommendation;
            List<string> secondaryOptions = GetPlanType(finalSecondaryRecommendation).Equals(REPLACEMENT_HEALTH) ? new() : _recommendationService.GetSecondaryOptions(quote);

            quote.Recommendations = new()
            {
                await GetRecommendation(primaryRecommendation, _recommendationService.GetPrimaryOptions(quote), quote),
                await GetRecommendation(finalSecondaryRecommendation, secondaryOptions, quote)
            };

            return quote;
        }

        public async Task<Recommendation> GetRecommendation(string plan, List<string> options, Quote quote)
        {
            return new()
            {
                PlanName = plan,
                PlanType = GetPlanType(plan),
                PlanDescription = await GetEmailDescription(plan),
                Price = await GetPrice(quote.Applicant.Province, GetApplicants(quote), GetProduct(plan, options)),
                Options = options,
                BuyNowLink = CreateBuyNowLink(plan, options, quote)
            };
        }

        public async Task<float> GetPrice(string province, List<Applicant> applicants, Product product)
        {
            PricingRequest pricingRequest = new()
            {
                Province = province,
                Applicants = applicants,
                Product = product
            };

            var response = await _httpClient.PostAsJsonAsync(PRICING_ENDPOINT, pricingRequest);

            var price = JsonConvert.DeserializeObject<Price>(await response.Content.ReadAsStringAsync());

            if (price is null || !response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception("Could not get price from api");
            }

            return price.MonthlyPremium;
        }

        public string CreateBuyNowLink(string plan, List<string> options, Quote quote)
        {
            List<int> pls = new();

            if (plan.Contains("Extenda"))
            {
                pls.Add((int)Pls.ExtendaPlan);
            }
            else {
                pls.Add((int)Enum.Parse(typeof(Pls), plan));
            }

            foreach (string option in options)
            {
                pls.Add((int)Enum.Parse(typeof(Pls), option));
            }

            BuyNowPayload buyPayload = new()
            {
                pr = quote.Applicant.Province,
                nod = (int)Enum.Parse(typeof(Dependants), quote.Questions.NumberPeopleCovered),
                pls = pls,
                aooa = Math.Max(quote.Applicant.ApplicantAge, quote.Applicant.SpouseAge),
                lep = quote.Questions.LosingGroupBenefits
            };

            return BUY_NOW_LINK+Base64Encode(buyPayload);
        }

        public string Base64Encode(object obj) => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));

        public Product GetProduct(string plan, List<string> options)
        {
            Product product =  new()
            {
                Plan = plan,
                DentalCoverage = options.Contains(DENTAL_CARE)
            };

            var drugCoverage = options.Find(o => o.Contains("Drug"));

            var travelCoverage = options.Find(o => o.Contains("Travel"));

            if (drugCoverage is not null)
            {
                product.DrugCoverage = drugCoverage;
            }

            if (travelCoverage is not null)
            {
                product.TravelCoverage = travelCoverage;
            }

            return product;

        }

        public List<Applicant> GetApplicants(Quote quote)
        {
            List<Applicant> applicants = new();

            var numberOfPeopleCovered = quote.Questions.NumberPeopleCovered;

            if (numberOfPeopleCovered.Contains("YOU"))
            {
                applicants.Add(new()
                {
                    Id = "1",
                    Birthdate = DateTime.UtcNow.AddYears(-quote.Applicant.ApplicantAge).ToString(ISO_8601_FORMAT)
                });
            }

            if (numberOfPeopleCovered.Contains("SPOUSE"))
            {
                applicants.Add(new()
                {
                    Id = "2",
                    Birthdate = DateTime.UtcNow.AddYears(-quote.Applicant.SpouseAge).ToString(ISO_8601_FORMAT)
                });
            }

            if (numberOfPeopleCovered.Contains("CHILD"))
            {
                applicants.Add(new()
                {
                    Id = "3",
                    Birthdate = DateTime.UtcNow.AddYears(-5).ToString(ISO_8601_FORMAT)
                });
            }

            if (numberOfPeopleCovered.Contains("CHILDREN"))
            {
                applicants.Add(new()
                {
                    Id = "4",
                    Birthdate = DateTime.UtcNow.AddYears(-5).ToString(ISO_8601_FORMAT)
                });
            }

            return applicants;
        }

        public async Task<string> GetEmailDescription(string planName) 
        {
            QueryDefinition query = new QueryDefinition("select t.description from t where t.name = @name").WithParameter("@name", planName);
            
            var description = await _cosmosService.GetFromDatabase(EMAIL_DESCRIPTIONS_CONTAINER, query, (List<dynamic> descriptions) => descriptions.FirstOrDefault());
           
            if (description is null) 
            {
                return string.Empty;
            }
            
            return description.description;
        }

        public string GetPlanType(string planName)
        {
            if(planName.Contains("Omni") || planName.Contains("Extenda") || planName.Contains("Basic"))
            {
                return PERSONAL_HEALTH;
            }

            return REPLACEMENT_HEALTH;
        }
    }
}