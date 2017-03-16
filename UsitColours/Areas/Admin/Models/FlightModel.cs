using System;
using AutoMapper;
using UsitColours.AutoMapper;
using UsitColours.Models;

namespace UsitColours.Areas.Admin.Models
{
    public class FlightModel : IHaveCustomMappings
    {
        public int AirportArrivalId { get; set; }

        public int AirportDepartureId { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public DateTime DateOfArrival { get; set; }

        public decimal Price { get; set; }

        public int AirlineId { get; set; }

        public int AvailableSeats { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<FlightModel, Flight>()
                 .ConstructUsing(x => new Flight(x.AirlineId, x.AirportArrivalId, x.AirportDepartureId, x.DateOfArrival, x.DateOfDeparture, x.Price, x.AvailableSeats));
        }
    }
}