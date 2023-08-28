using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health
{
    public interface IMentalHealthRecommendation
    {
        public string GetPrimaryMentalHealthPlan(Quote quote);

        public string GetSecondaryMentalHealthPlan(Quote quote);
    }
}
