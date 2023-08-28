using Gmsca.HelpMeChoose.Individual.Models.Validation;
using Gmsca.HelpMeChoose.Individual.Interfaces;

namespace Gmsca.HelpMeChoose.Individual.Models
{
    public class Applicant : IApplicant
    {
        public int ApplicantAge { get; set; }
        [StringRange(AllowableValues = new[] { "BC", "AB", "SK", "MB", "ON", "NS", "PE", "NL", "YT", "NT", "", null })]
        public string Province { get; set; } = string.Empty;
        public int SpouseAge { get; set; }
    }
}
