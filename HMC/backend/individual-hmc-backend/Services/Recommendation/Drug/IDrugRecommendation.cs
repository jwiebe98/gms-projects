using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug
{
    public interface IDrugRecommendation
    {
        public string GetPrimaryDrugPlan(Quote quote);

        public string GetPrimaryDrugOption(Quote quote);

        public string GetSecondaryDrugPlan();

        public string GetSecondaryDrugOption(Quote quote);
    }
}
