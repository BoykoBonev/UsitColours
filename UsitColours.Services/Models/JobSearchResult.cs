using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Models
{
    public class JobSearchResult
    {
        public IEnumerable<Job> Jobs { get; set; }

        public int Count { get; set; }
    }
}
