using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health
{
    public interface IHealthRecommendation
    {
        public string GetPrimaryHealthPlan(Quote quote);

        public string GetSecondaryHealthPlan(Quote quote);
    }
}
