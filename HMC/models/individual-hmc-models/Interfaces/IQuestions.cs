namespace Gmsca.HelpMeChoose.Individual.Interfaces
{
    public interface IQuestions
    {
        public string NumberPeopleCovered { get; set; } 
        public string NumberOfDrugPrescriptions { get; set; } 
        public List<string> HealthCarePractitionerType { get; set; } 
        public string FrequencyOfMentalHealthVisits { get; set; } 
        public string TravelDuration { get; set; } 
        public string HealthyLifestyle { get; set; } 
        public bool LosingGroupBenefits { get; set; }
        public List<string> CoverageType { get; set; } 
        public bool ExistingPrescription { get; set; }
    }
}
