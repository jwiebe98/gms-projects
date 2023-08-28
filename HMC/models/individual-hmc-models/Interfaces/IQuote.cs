using Gmsca.HelpMeChoose.Individual.Models;
using System.Globalization;

namespace Gmsca.HelpMeChoose.Individual.Interfaces
{
    public interface IQuote
    {
        public string id { get; set; }         
        public string QuoteId { get; set; }         
        public bool IsCompleted { get; set; }
        public string Timestamp { get; set; }         
        public string Email { get; set; }
        public Applicant Applicant { get; set; }         
        public Questions Questions { get; set; }         
        public List<Recommendation> Recommendations { get; set; }     
    }
}
