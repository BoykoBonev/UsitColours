using System;
using AutoMapper;
using UsitColours.AutoMapper;
using UsitColours.Models;

namespace UsitColours.Areas.Admin.Models
{
    public class AirportViewModel: IHaveCustomMappings, IMapFrom<Airport>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CityId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<AirportViewModel, Airport>()
                 .ConstructUsing(x => new Airport(x.Name, int.Parse(x.CityId)));
        }
    }
}