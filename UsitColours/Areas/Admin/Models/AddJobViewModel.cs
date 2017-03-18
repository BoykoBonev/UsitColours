using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UsitColours.Areas.Admin.Models
{
    public class AddJobViewModel
    {
        public string JobDescription { get; set; }

        public string JobTitle { get; set; }

        public decimal Wage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CompanyName { get; set; }

        public decimal Price { get; set; }

        public int Slots { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public int CityId { get; set; }

        public bool IsDefaultImage { get; set; }

    }
}