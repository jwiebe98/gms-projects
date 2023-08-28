using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Vision
{
    public class VisionRecommendation : IVisionRecommendation
    {
        public string GetPrimaryVisionPlan(Quote quote)
        {
            var needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            var needsVision = quote.Questions.CoverageType.Contains(VISION);
            var province = quote.Applicant.Province;

            return needsReplacementHealth && needsVision ? CHOICE :
                !needsReplacementHealth && !needsVision ? BASIC :
                needsReplacementHealth && !needsVision ? ESSENTIAL :
                !needsReplacementHealth && needsVision && province.Equals(SK) ? EXTENDA_PLAN_SK_OPTION1 :
                !needsReplacementHealth && needsVision && !province.Equals(SK) ? EXTENDA_PLAN :
                throw new Exception("Unknown Primary Vision Plan");
        }

        public string GetSecondaryVisionPlan(Quote quote)
        {
            var needsVision = quote.Questions.CoverageType.Contains(VISION);
            var province = quote.Applicant.Province;

            return !needsVision ? BASIC :
                !province.Equals(SK) ? EXTENDA_PLAN :
                province.Equals(SK) ? EXTENDA_PLAN_SK_OPTION1 :
                throw new Exception("Unknown Secondary Vision Plan");
        }
    }
}
