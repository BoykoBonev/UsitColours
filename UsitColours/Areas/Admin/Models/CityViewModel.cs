using System;
using AutoMapper;
using UsitColours.AutoMapper;
using UsitColours.Models;
using System.ComponentModel.DataAnnotations;

namespace UsitColours.Areas.Admin.Models
{
    public class CityViewModel:IMapFrom<City>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, ErrorMessage ="The {0} must be at least {1} characters long", MinimumLength = 2)]
        [Display(Name ="City name")]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<CityViewModel, City>()
                 .ConstructUsing(x => new City(x.Name, x.CountryId));
        }
    }
}