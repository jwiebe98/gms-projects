using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation
{
    public interface IRecommendationService
    {
        public string GetPrimaryRecommendation(Quote quote);
        public List<string> GetPrimaryOptions(Quote quote);
        public List<string> GetSecondaryOptions(Quote quote);
        public string GetSecondaryRecommendation(Quote quote);
        public string GetDifferentSecondaryRecommendation(Quote quote);
    }
}
