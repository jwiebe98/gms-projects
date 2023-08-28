using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision
{
    public interface IVisionRecommendation
    {
        public string GetPrimaryVisionPlan(Quote quote);
        public string GetSecondaryVisionPlan(Quote quote);
    }
}
