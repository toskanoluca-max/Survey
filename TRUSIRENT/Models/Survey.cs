using System.Collections.Generic;

namespace YourApp.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<Option> Options { get; set; } = new();
    }
}