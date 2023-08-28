using Gmsca.HelpMeChoose.Individual.Interfaces;
using Gmsca.HelpMeChoose.Individual.Models.Validation;
using System.Globalization;

namespace Gmsca.HelpMeChoose.Individual.Models
{
    [SpouseAgeValidator]
    public class Quote : IQuote
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string QuoteId { get; set; } = Guid.NewGuid().ToString();
        public bool IsCompleted { get; set; }
        public string Timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public Applicant Applicant { get; set; } = new Applicant();
        public Questions Questions { get; set; } = new Questions();
        public List<Recommendation> Recommendations { get; set; } = new();
    }
}