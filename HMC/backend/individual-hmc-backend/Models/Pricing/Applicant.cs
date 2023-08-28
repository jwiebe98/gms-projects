using Gmsca.HelpMeChoose.Individual.Interfaces.Pricing;

namespace Gmsca.HelpMeChoose.Individual.Models.Pricing
{
    public class Applicant : IApplicant
    {
        public string Id { get; set; } = string.Empty;
        public string Birthdate { get; set; } = string.Empty;
    }
}
