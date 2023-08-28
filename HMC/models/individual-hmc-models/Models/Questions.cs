using Gmsca.HelpMeChoose.Individual.Models.Validation;
using Gmsca.HelpMeChoose.Individual.Interfaces;

namespace Gmsca.HelpMeChoose.Individual.Models
{
    [HealthPractitionersValidator]
    [MentalHealthValidator]
    [PrescriptionMedicationValidator]
    [TravelValidator]
    public class Questions : IQuestions
    {
        [StringRange(AllowableValues = new[] { "YOU", "YOU_YOUR_SPOUSE", "YOU_YOUR_SPOUSE_YOUR_CHILDREN", "YOU_YOUR_CHILD", "YOU_YOUR_CHILDREN", "", null })]
        public string NumberPeopleCovered { get; set; } = string.Empty;

        [StringRange(AllowableValues = new[] { "JUST_FOR_EMERGENCIES", "1_OR_2", "3_PLUS", "", null })]
        public string NumberOfDrugPrescriptions { get; set; } = string.Empty;

        [StringArray(new[] { "MASSAGE", "CHIROPRACTOR", "PHYSIOTHERAPIST", "SPEECH_PATHOLOGIST", "ACUPUNCTURIST", "NATUROPATH", "DIETITIAN", "OSTEOPATH", "CHIROPODIST_PODIATRIST" })]
        public List<string> HealthCarePractitionerType { get; set; } = new();

        [StringRange(AllowableValues = new[] { "1_TO_3", "4_TO_8", "9_PLUS", "", null })]
        public string FrequencyOfMentalHealthVisits { get; set; } = string.Empty;

        [StringRange(AllowableValues = new[] { "LESS_THAN_ONE_WEEK", "1_TO_2_WEEKS", "2_TO_4_WEEKS", "1_TO_2_MONTHS", "2_PLUS_MONTHS", "", null })]
        public string TravelDuration { get; set; } = string.Empty;

        [StringRange(AllowableValues = new[] { "ALREADY_DOING_IT", "NOT_INTERESTED", "LOOKING_TO_GET_STARTED", "", null })]
        public string HealthyLifestyle { get; set; } = string.Empty;

        public bool LosingGroupBenefits { get; set; }

        [StringArray(new[] { "PRESCRIPTION_MEDICATION", "TRAVEL", "HEALTH_PRACTITIONERS", "VISION", "DENTAL", "MENTAL_HEALTH_SUPPORT", "EMERGENCY", })]
        public List<string> CoverageType { get; set; } = new();

        public bool ExistingPrescription { get; set; }
    }
}
