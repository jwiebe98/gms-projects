using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health
{
    public class HealthRecommendation : IHealthRecommendation
    {
        public string GetPrimaryHealthPlan( Quote quote)
        {
            bool needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            bool needsHealth = quote.Questions.CoverageType.Contains(HEALTH_PRACTITIONERS);
            string province = quote.Applicant.Province;
            bool needsMassageChiroPhysio = quote.Questions.HealthCarePractitionerType.Where(p => p.Equals(CHIROPRACTOR) || p.Equals(MASSAGE) || p.Equals(PHYSIOTHERAPIST)).Count() != 0;

            if (needsReplacementHealth)
            {
                if (needsMassageChiroPhysio)
                {
                    return PREMIER;
                }
                else
                {
                    return ESSENTIAL;
                }
            }
            else if (!needsHealth)
            {
                return BASIC;
            }
            else if (needsMassageChiroPhysio)
            {
                return OMNI_PLAN;
            }
            else if (province.Equals(SK))
            {
                return EXTENDA_PLAN_SK_OPTION1;
            }
            else
            {
                return EXTENDA_PLAN;
            }
        }

        public string GetSecondaryHealthPlan( Quote quote)
        {
            bool needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            bool needsHealth = quote.Questions.CoverageType.Contains(HEALTH_PRACTITIONERS);
            string province = quote.Applicant.Province;
            bool needsMassageChiroPhysio = quote.Questions.HealthCarePractitionerType.Where(p => p.Equals(CHIROPRACTOR) || p.Equals(MASSAGE) || p.Equals(PHYSIOTHERAPIST)).Count() != 0;

            if ((needsReplacementHealth && !needsHealth) || (!needsReplacementHealth && needsHealth && !needsMassageChiroPhysio))
            {
                return BASIC;
            }
            else if (needsReplacementHealth && needsHealth && needsMassageChiroPhysio) {
                return OMNI_PLAN;
            }
            else if (province.Equals(SK)) {
                return EXTENDA_PLAN_SK_OPTION1;
            }
            else
            {
                return EXTENDA_PLAN;
            }
        }
    }
}
