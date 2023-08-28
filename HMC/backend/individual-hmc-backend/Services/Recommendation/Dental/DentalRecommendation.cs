using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Dental;

public class DentalRecommendation : IDentalRecommendation
{
    public string GetPrimaryDentalPlan(Quote quote)
    {
        var needsReplacementHealth = quote.Questions.LosingGroupBenefits;
        var needsDental = quote.Questions.CoverageType.Contains(DENTAL);

        return !needsReplacementHealth ? BASIC :
            !needsDental ? ESSENTIAL :
            needsDental ? PREMIER :
            throw new Exception("Unknown Primary Dental Plan");
    }

    public string GetPrimaryDentalOption(Quote quote)
    {
        var needsReplacementHealth = quote.Questions.LosingGroupBenefits;
        var needsDental = quote.Questions.CoverageType.Contains(DENTAL);

        return !needsReplacementHealth && needsDental ? DENTAL_CARE : NONE;
    }

    public string GetSecondaryDentalPlan()
    {
        return BASIC;
    }

    public string GetSecondaryDentalOption(Quote quote)
    {
        var needsDental = quote.Questions.CoverageType.Contains(DENTAL);

        return needsDental ? DENTAL_CARE : NONE;
    }
}


