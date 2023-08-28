using Gmsca.HelpMeChoose.Individual.Models;
using static Gmsca.HelpMeChoose.Individual.Constants.Content;

namespace Gmsca.HelpMeChoose.Individual.Services.PlanRecommendation.Drug
{
    public class DrugRecommendation : IDrugRecommendation
    {
        public string GetPrimaryDrugPlan(Quote quote)
        {
            var needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            var numberOfPerscriptionDrugs = quote.Questions.NumberOfDrugPrescriptions;
            var needsDrug = quote.Questions.CoverageType.Contains(PRESCRIPTION_MEDICATION);

            return !needsReplacementHealth ? BASIC :
                    needsReplacementHealth && !needsDrug ? ESSENTIAL :
                    numberOfPerscriptionDrugs == THREE_PLUS ? PREMIER :
                    CHOICE;
        }

        public string GetPrimaryDrugOption(Quote quote)
        {
            var needsReplacementHealth = quote.Questions.LosingGroupBenefits;
            var needsDrug = quote.Questions.CoverageType.Contains(PRESCRIPTION_MEDICATION);
            var existingPrescription = quote.Questions.ExistingPrescription;

            return needsReplacementHealth || !needsDrug ? NONE :
                existingPrescription ? ENHANCED_DRUG :
                BASIC_DRUG;

        }

        public string GetSecondaryDrugPlan()
        {
            return BASIC;
        }

        public string GetSecondaryDrugOption(Quote quote)
        {
            var needsDrug = quote.Questions.CoverageType.Contains(PRESCRIPTION_MEDICATION);
            var existingPrescription = quote.Questions.ExistingPrescription;

            return !needsDrug ? NONE :
                existingPrescription ? ENHANCED_DRUG :
                BASIC_DRUG;

        }
    }
}


