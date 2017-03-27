using System;
using AutoMapper;
using UsitColours.AutoMapper;
using UsitColours.Models;
using System.ComponentModel.DataAnnotations;
using UsitColours.Constants;

namespace UsitColours.Areas.Admin.Models
{
    public class CityViewModel:IMapFrom<City>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.CityMaxLength, ErrorMessage ="The {0} must be at least {1} characters long", MinimumLength = GlobalConstants.CityMinLength)]
        [Display(Name ="City name")]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<CityViewModel, City>()
                 .ConstructUsing(x => new City(x.Name, x.CountryId));
        }
    }
}