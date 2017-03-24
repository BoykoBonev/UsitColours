using System;
using UsitColours.AutoMapper;
using UsitColours.Models;

namespace UsitColours.Areas.Profile.Models
{
    public class JobStatisticViewModel: IMapFrom<Job>
    {
        public string JobTitle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }
        public decimal Wage { get; set; }

        public string CompanyName { get; set; }

    }
}