using System.Collections.Generic;

namespace UsitColours.Models
{
    public class SearchJobViewModel
    {
        public int Count { get; set; }

        public int page { get; set; }

        public IEnumerable<JobBaseViewModel> Jobs { get; set; }

        public string SearchTerm { get; set; }
    }
}