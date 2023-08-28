using Gmsca.HelpMeChoose.Individual.Models;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Health;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel;
using Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision;

using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation
{
    public class RecommendationService : IRecommendationService
    {
        IVisionRecommendation _visionRecommendation;
        ITravelRecommendation _travelRecommendation;
        IHealthRecommendation _healthRecommendation;
        IMentalHealthRecommendation _mentalHealthRecommendation;
        IDrugRecommendation _drugRecommendation;
        IDentalRecommendation _dentalRecommendation;

        public RecommendationService(
            IVisionRecommendation visionRecommendation, 
            ITravelRecommendation travelRecommendation, 
            IHealthRecommendation healthRecommendation, 
            IMentalHealthRecommendation mentalHealthRecommendation, 
            IDrugRecommendation drugRecommendation, 
            IDentalRecommendation dentalRecommendation
            )
        {
            _visionRecommendation = visionRecommendation;
            _travelRecommendation = travelRecommendation;
            _healthRecommendation = healthRecommendation;
            _mentalHealthRecommendation = mentalHealthRecommendation;
            _drugRecommendation = drugRecommendation;
            _dentalRecommendation = dentalRecommendation;
        }

        public string GetPrimaryRecommendation(Quote quote)
        {

            var travelPlan = _travelRecommendation.GetPrimaryTravelPlan(quote); 

            List< string> plans = new()
            {
                _healthRecommendation.GetPrimaryHealthPlan(quote),
                _mentalHealthRecommendation.GetPrimaryMentalHealthPlan(quote),
                _dentalRecommendation.GetPrimaryDentalPlan(quote),
                _visionRecommendation.GetPrimaryVisionPlan(quote),
                _drugRecommendation.GetPrimaryDrugPlan(quote),
            };

            if (travelPlan.Equals(PREMIER) || plans.Contains(PREMIER))
            {
                return PREMIER_HEALTH;
            }
            else if (plans.Contains(OMNI_PLAN)) {
                return OMNI_PLAN;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN_SK_PLUS))
            {
                return EXTENDA_PLAN_SK_PLUS;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN_SK_OPTION2))
            {
                return EXTENDA_PLAN_SK_OPTION2;
            }
            else if (travelPlan.Equals(OMNI_PLAN))
            {
                return OMNI_PLAN;
            }
            else if (travelPlan.Equals(CHOICE) || plans.Contains(CHOICE))
            {
                return CHOICE_HEALTH;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN_SK_OPTION1) || plans.Contains(EXTENDA_PLAN_SK_OPTION1))
            {
                return EXTENDA_PLAN_SK_OPTION1;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN) || plans.Contains(EXTENDA_PLAN))
            {
                return EXTENDA_PLAN;
            }
            else if (travelPlan.Equals(RH_BASIC))
            {
                return BASIC_PLAN;
            }
            else if (travelPlan.Equals(ESSENTIAL) || plans.Contains(ESSENTIAL))
            {
                return ESSENTIAL_HEALTH;
            }
            else
            {
                return BASIC_PLAN;
            }
        }

        public List<string> GetPrimaryOptions(Quote quote)
        {
            var primaryPlan = GetPrimaryRecommendation(quote);

            List<string> rhPlans = new List<string>()
            {
                ESSENTIAL_HEALTH,
                CHOICE_HEALTH,
                PREMIER_HEALTH
            };

            if (rhPlans.Contains(primaryPlan))
            {
                return new();
            }

            List<string> options = new()
            {
                _drugRecommendation.GetPrimaryDrugOption(quote),
                _dentalRecommendation.GetPrimaryDentalOption(quote),
                _travelRecommendation.GetPrimaryTravelOption(quote)
            };

            return options.FindAll(o => !o.Equals(NONE));
        }

        public List<string> GetSecondaryOptions(Quote quote)
        {
            var secondary = GetSecondaryRecommendation(quote);

            List<string> rhPlans = new List<string>()
            {
                ESSENTIAL_HEALTH,
                CHOICE_HEALTH,
                PREMIER_HEALTH
            };

            if (rhPlans.Contains(secondary))
            {
                return new();
            }

            List<string> options = new()
            {
                _dentalRecommendation.GetSecondaryDentalOption(quote),
                _travelRecommendation.GetSecondaryTravelOption(quote),
                _drugRecommendation.GetSecondaryDrugOption(quote)
            };

            return options.FindAll(o => !o.Equals(NONE));
        }

        public string GetDifferentSecondaryRecommendation(Quote quote)
        {
            var primaryRecommendation = GetPrimaryRecommendation(quote);
            bool needsTravel = quote.Questions.CoverageType.Contains(TRAVEL);
            string province = quote.Applicant.Province;
            string travelDuration = quote.Questions.TravelDuration;

            if (primaryRecommendation.Contains(EXTENDA_PLAN))
            {
                return OMNI_PLAN;
            }

            if (primaryRecommendation.Equals(BASIC_PLAN))
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

            if (primaryRecommendation.Equals(PREMIER_HEALTH) || primaryRecommendation.Equals(ESSENTIAL_HEALTH))
            {
                return CHOICE_HEALTH;
            }

            if (primaryRecommendation.Equals(CHOICE_HEALTH))
            {
                return PREMIER_HEALTH;
            }

            if (primaryRecommendation.Equals(OMNI_PLAN))
            {
                if (!province.Equals(SK))
                {
                    return EXTENDA_PLAN;
                }
                else if (!needsTravel)
                {
                    return EXTENDA_PLAN_SK_OPTION1;
                }
                else if (travelDuration.Equals(LESS_THAN_ONE_WEEK))
                {
                    return EXTENDA_PLAN_SK_OPTION1;
                }
                else if (travelDuration.Equals(ONE_TO_TWO_WEEKS) || travelDuration.Equals(TWO_TO_FOUR_WEEKS))
                {
                    return EXTENDA_PLAN_SK_OPTION2;
                }
                else if (travelDuration.Equals(ONE_TO_TWO_MONTHS) || travelDuration.Equals(TWO_PLUS_MONTHS))
                {
                    return EXTENDA_PLAN_SK_PLUS;
                }
            }

            throw new Exception("Couldn't find secondary recommendation.");
        }

        public string GetSecondaryRecommendation(Quote quote)
        {
            var travelPlan = _travelRecommendation.GetSecondaryTravelPlan(quote);

            List<string> plans = new()
            {
                _healthRecommendation.GetSecondaryHealthPlan(quote),
                _mentalHealthRecommendation.GetSecondaryMentalHealthPlan(quote),
                _dentalRecommendation.GetSecondaryDentalPlan(),
                _visionRecommendation.GetSecondaryVisionPlan(quote),
                _drugRecommendation.GetSecondaryDrugPlan(),
            };

            if (travelPlan.Equals(PREMIER) || plans.Contains(PREMIER))
            {
                return PREMIER_HEALTH;
            }
            else if (plans.Contains(OMNI_PLAN))
            {
                return OMNI_PLAN;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN_SK_PLUS))
            {
                return EXTENDA_PLAN_SK_PLUS;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN_SK_OPTION2))
            {
                return EXTENDA_PLAN_SK_OPTION2;
            }
            else if (travelPlan.Equals(OMNI_PLAN))
            {
                return OMNI_PLAN;
            }
            else if (travelPlan.Equals(CHOICE) || plans.Contains(CHOICE))
            {
                return CHOICE_HEALTH;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN_SK_OPTION1) || plans.Contains(EXTENDA_PLAN_SK_OPTION1))
            {
                return EXTENDA_PLAN_SK_OPTION1;
            }
            else if (travelPlan.Equals(EXTENDA_PLAN) || plans.Contains(EXTENDA_PLAN))
            {
                return EXTENDA_PLAN;
            }
            else if (travelPlan.Equals(ESSENTIAL) || plans.Contains(ESSENTIAL))
            {
                return ESSENTIAL_HEALTH;
            }
            else
            {
                return BASIC_PLAN;
            }
        }
    }
}
