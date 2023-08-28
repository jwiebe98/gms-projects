using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health
{
    public class MentalHealthRecommendation : IMentalHealthRecommendation
    {
        public string GetPrimaryMentalHealthPlan(Quote quote)
        {
            bool needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            bool needsMentalHealth = quote.Questions.CoverageType.Contains(MENTAL_HEALTH_SUPPORT);
            string province = quote.Applicant.Province;
            string frequency = quote.Questions.FrequencyOfMentalHealthVisits;

            if (needsReplacementHealth)
            {
                if (!needsMentalHealth && frequency.Equals(""))
                {
                    return ESSENTIAL;
                }
                else if (frequency.Equals(ONE_TO_THREE))
                {
                    return CHOICE;
                }
                else
                {
                    return PREMIER;
                }
            }
            else if (!needsMentalHealth)
            {
                return BASIC;
            }
            else if (frequency.Equals(ONE_TO_THREE))
            {
                if (province.Equals(SK))
                {
                    return EXTENDA_PLAN_SK_OPTION1;
                }
                else
                {
                    return EXTENDA_PLAN;
                }
            }
            else
            {
                return OMNI_PLAN;
            }
        }

        public string GetSecondaryMentalHealthPlan(Quote quote)
        {
            bool needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            bool needsMentalHealth = quote.Questions.CoverageType.Contains(MENTAL_HEALTH_SUPPORT);
            string province = quote.Applicant.Province;
            string frequency = quote.Questions.FrequencyOfMentalHealthVisits;

            if (!needsMentalHealth)
            {
                if (needsReplacementHealth)
                {
                    return BASIC;
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
            else if (needsReplacementHealth)
            {
                if (frequency.Equals(ONE_TO_THREE))
                {
                    if (province.Equals(SK))
                    {
                        return EXTENDA_PLAN_SK_OPTION1;
                    }
                    else
                    {
                        return EXTENDA_PLAN;
                    }
                }
                else
                {
                    return OMNI_PLAN;
                }
            }
            else if (frequency.Equals(ONE_TO_THREE))
            {
                return OMNI_PLAN;
            }
            else
            {
                if (province.Equals(SK))
                {
                    return EXTENDA_PLAN_SK_OPTION1;
                }
                else
                {
                    return EXTENDA_PLAN;
                }
            }

            throw new Exception("Couldn't pick mental health secondary plan");
        }
    }
}
