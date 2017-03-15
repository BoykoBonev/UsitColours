using System.Collections.Generic;
using System.Web.Mvc;

namespace UsitColours.Areas.Admin.Models
{
    public class AddAirportViewModel
    {
        public List<SelectListItem> Countries { get; set; }

        public List<SelectListItem> Cities { get; set; }

        public string Name { get; set; }
    }
}