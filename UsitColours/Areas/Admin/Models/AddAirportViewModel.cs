using System.Collections.Generic;
using System.Web.Mvc;

namespace UsitColours.Areas.Admin.Models
{
    public class AddAirportViewModel
    {
        public List<SelectListItem> Countries { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }
    }
}