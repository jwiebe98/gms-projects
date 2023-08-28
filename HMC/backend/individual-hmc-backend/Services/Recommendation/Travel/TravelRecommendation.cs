using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;


namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Travel
{
    public class TravelRecommendation : ITravelRecommendation
    {
        public string GetPrimaryTravelPlan(Quote quote)
        {
            bool needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            bool needsTravel = quote.Questions.CoverageType.Contains(TRAVEL);
            string province = quote.Applicant.Province;
            string travelDuration = quote.Questions.TravelDuration;

            if (needsReplacementHealth)
            {
                if (!needsTravel)
                {
                    return ESSENTIAL;
                }
                else if (travelDuration.Equals(LESS_THAN_ONE_WEEK))
                {
                    return CHOICE;
                }
                else if (travelDuration.Equals(ONE_TO_TWO_WEEKS))
                {
                    return PREMIER;
                }
                else if (province.Equals(SK)) 
                {
                    if (travelDuration.Equals(TWO_TO_FOUR_WEEKS))
                    {
                        return EXTENDA_PLAN_SK_OPTION2;
                    }
                    else
                    {
                        return EXTENDA_PLAN_SK_PLUS;
                    }
                }
                else
                {
                    return RH_BASIC;
                }
            }
            else
            {
                if (!needsTravel)
                {
                    return BASIC;
                }
                else if (!province.Equals(SK))
                {
                    return EXTENDA_PLAN;
                }
                else if (travelDuration.Equals(ONE_TO_TWO_MONTHS) || travelDuration.Equals(TWO_PLUS_MONTHS))
                {
                    return EXTENDA_PLAN_SK_PLUS;
                }
                else
                {
                    return EXTENDA_PLAN_SK_OPTION2;
                }
            }
            
        }

        public string GetPrimaryTravelOption(Quote quote)
        {
            bool needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            bool needsTravel = quote.Questions.CoverageType.Contains(TRAVEL);
            string province = quote.Applicant.Province;
            string travelDuration = quote.Questions.TravelDuration;

            if (needsTravel && !province.Equals(SK))
            {
                if (needsReplacementHealth)
                {
                    switch (travelDuration)
                    {
                        case TWO_TO_FOUR_WEEKS:
                            return TRAVEL_UP_TO_30_DAYS;
                        case ONE_TO_TWO_MONTHS:
                        case TWO_PLUS_MONTHS:
                            return TRAVEL_UP_TO_48_DAYS;
                        default:
                            return NONE;
                    }
                } 
                else
                {
                    switch (travelDuration)
                    {
                        case LESS_THAN_ONE_WEEK:
                        case ONE_TO_TWO_WEEKS:
                            return TRAVEL_UP_TO_15_DAYS;
                        case TWO_TO_FOUR_WEEKS:
                            return TRAVEL_UP_TO_30_DAYS;
                        case ONE_TO_TWO_MONTHS:
                        case TWO_PLUS_MONTHS:
                            return TRAVEL_UP_TO_48_DAYS;
                        default:
                            return NONE;
                    }
                }
            }

            return NONE;
        }

        public string GetSecondaryTravelOption(Quote quote)
        {
            bool needsTravel = quote.Questions.CoverageType.Contains(TRAVEL);
            string province = quote.Applicant.Province;
            string travelDuration = quote.Questions.TravelDuration;

            if (needsTravel && !province.Equals(SK))
            {
                switch (travelDuration)
                {
                    case LESS_THAN_ONE_WEEK:
                    case ONE_TO_TWO_WEEKS:
                        return TRAVEL_UP_TO_15_DAYS;
                    case TWO_TO_FOUR_WEEKS:
                        return TRAVEL_UP_TO_30_DAYS;
                    case ONE_TO_TWO_MONTHS:
                    case TWO_PLUS_MONTHS:
                        return TRAVEL_UP_TO_48_DAYS;
                    default:
                        return NONE;
                }
            }

            return NONE;
        }

        public string GetSecondaryTravelPlan(Quote quote)
        {
            bool needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            bool needsTravel = quote.Questions.CoverageType.Contains(TRAVEL);
            string province = quote.Applicant.Province;
            string travelDuration = quote.Questions.TravelDuration;

            if (province.Equals(SK) && needsTravel)
            {
                switch (travelDuration)
                {
                    case ONE_TO_TWO_MONTHS:
                    case TWO_PLUS_MONTHS:
                        return EXTENDA_PLAN_SK_PLUS;
                    default:
                        return EXTENDA_PLAN_SK_OPTION2;
                }
            }
            else if (!province.Equals(SK) && needsTravel && !needsReplacementHealth)
            {
                return EXTENDA_PLAN;
            }
            else
            {
                return BASIC;
            }
        }
    }
}
