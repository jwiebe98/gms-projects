using GMS.CIMS.BenefitsRemaining.Models;
using System.Collections.Generic;
using System.Linq;

namespace GMS.CIMS.BenefitsRemaining
{
    public static class GroupBenefitsRemainingByBenefitName
    {
        public static List<BenefitRemaining> GetGroupedBenefitsRemainingByBenefitName(this List<BenefitRemaining> benefitsRemaining)
        {
            var benefitsRemainingGroupedByName = benefitsRemaining.GroupBy(br => br.Name);

            foreach (var benefitGroupedByName in benefitsRemainingGroupedByName)
            {
                if (benefitGroupedByName.Count() > 1)
                {
                    var nonGroupedBenefits = benefitsRemaining.FindAll(br => br.Name != benefitGroupedByName.Key);

                    nonGroupedBenefits.Add(new()
                    {
                        Name = benefitGroupedByName.Key,
                        Practitioners = benefitGroupedByName.SelectMany(br => br.Practitioners).ToList(),
                        DisplayPriority = benefitGroupedByName.First().DisplayPriority
                    });

                    benefitsRemaining = nonGroupedBenefits;
                }
            }

            return benefitsRemaining;
        }
    }
}
