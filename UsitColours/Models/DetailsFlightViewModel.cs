using System;
using AutoMapper;
using UsitColours.AutoMapper;
using UsitColours.Services.Models;

namespace UsitColours.Models
{

    public class DetailsFlightViewModel : IMapFrom<Flight>, IMapFrom<PresentationFlight>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AirportDepartureName { get; set; }

        public string AirportArrivalName { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public DateTime DateOfArrival { get; set; }

        public decimal Price { get; set; }

        public int AvailableSeats { get; set; }

        public string AirlineName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<DetailsFlightViewModel, PresentationFlight>()
                .ConstructUsing(x => new PresentationFlight(x.Id, string.Empty, string.Empty, x.AirportDepartureName, x.AirportArrivalName, x.DateOfDeparture, x.DateOfArrival, x.Price, x.AirlineName, x.AvailableSeats));
        }
    }
}