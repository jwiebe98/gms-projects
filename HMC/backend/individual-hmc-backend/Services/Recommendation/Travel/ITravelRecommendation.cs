using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel
{
    public interface ITravelRecommendation
    {
        public string GetPrimaryTravelPlan(Quote quote);

        public string GetPrimaryTravelOption(Quote quote);

        public string GetSecondaryTravelOption(Quote quote);

        public string GetSecondaryTravelPlan(Quote quote);
    }
}
