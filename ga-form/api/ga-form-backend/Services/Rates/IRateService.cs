using Newtonsoft.Json.Linq;

namespace Gmsca.Group.GA.Backend.Services.Rates
{
    public interface IRateService
    {
        Task<JObject> GetEffectiveRates();
    }
}
