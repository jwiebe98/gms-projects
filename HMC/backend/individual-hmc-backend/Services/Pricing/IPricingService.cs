using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.Pricing
{
    public interface IPricingService
    {
        Task<Quote> GetPrices(Quote quote);
    }
}
