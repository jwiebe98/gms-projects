using System.Globalization;
using static Gmsca.HelpMeChoose.Individual.Constants.Constants;

namespace Gmsca.HelpMeChoose.Individual.Models.BuyNowPayload
{
    public class BuyNowPayload
    {
        public bool lep { get; set; } = false;
        public int bId { get; } = 0;
        public string pr { get; set; } = string.Empty;
        public int nod { get; set; } = 0;
        public string ed { get; } = DateTime.UtcNow.ToString(ISO_8601_FORMAT, CultureInfo.InvariantCulture);
        public List<int> pls { get; set; } = new();
        public int aooa { get; set; }
    }

    public enum Pls
    {
        BasicPlan = 1,
        ExtendaPlan = 2,
        OmniPlan = 3,
        PrescriptionDrugBasic = 4,
        DentalCare = 5,
        HOSPITAL_CASH = 6,
        Travel15DaysPerTrip = 7,
        Travel30DaysPerTrip = 9,
        Travel48daysPerTrip = 10,
        ExtendaPlan_SKOption1 = 11,
        ExtendaPlan_SKOption2 = 12,
        ExtendaPlan_SKPlus = 13,
        PrescriptionDrugEnhanced = 14,
        ChoiceHealth = 17,
        EssentialHealth = 20,
        PremierHealth = 23
    }

    public enum Dependants
    {
        YOU = 1,
        YOU_YOUR_SPOUSE = 2,
        YOU_YOUR_SPOUSE_YOUR_CHILDREN = 4,
        YOU_YOUR_CHILD = 2,
        YOU_YOUR_CHILDREN = 3
    }
}
