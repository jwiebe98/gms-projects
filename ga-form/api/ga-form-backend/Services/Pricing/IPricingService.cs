using Gmsca.Group.GA.Models;

namespace Gmsca.Group.GA.Backend.Services.Pricing
{
    public interface IPricingService
    {
        Task<Quote> SetPricesInQuote(Quote quote);
    }
}
