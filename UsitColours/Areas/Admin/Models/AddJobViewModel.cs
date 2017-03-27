using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UsitColours.Constants;

namespace UsitColours.Areas.Admin.Models
{
    public class AddJobViewModel
    {
        [StringLength(GlobalConstants.JobDescriptionMaxLength, ErrorMessage = "The {0} must be at least {1} characters long", MinimumLength = GlobalConstants.JobDescriptionMinLength)]
        public string JobDescription { get; set; }

        [StringLength(GlobalConstants.JobTitleMaxLength, ErrorMessage = "The {0} must be at least {1} characters long", MinimumLength = GlobalConstants.JobTitleMinLength)]
        public string JobTitle { get; set; }

        public decimal Wage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(GlobalConstants.CompanyMaxLength, ErrorMessage = "The {0} must be at least {1} characters long", MinimumLength = GlobalConstants.CompanyMinLength)]
        public string CompanyName { get; set; }

        public decimal Price { get; set; }

        public int Slots { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public int CityId { get; set; }

        public bool IsDefaultImage { get; set; }

    }
}