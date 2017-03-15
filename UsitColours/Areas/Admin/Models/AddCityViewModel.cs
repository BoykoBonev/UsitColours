using System.Collections.Generic;
using System.Web.Mvc;

namespace UsitColours.Areas.Admin.Models
{
    public class AddCityViewModel
    {
        public string Name { get; set; }

        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }
    }
}