using Gmsca.HelpMeChoose.Individual.Models;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental
{
    public interface IDentalRecommendation
    {
        public string GetPrimaryDentalPlan(Quote quote);

        public string GetPrimaryDentalOption(Quote quote);

        public string GetSecondaryDentalPlan();

        public string GetSecondaryDentalOption(Quote quote);
    }
}
