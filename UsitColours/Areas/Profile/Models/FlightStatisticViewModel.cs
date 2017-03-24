using System;
using UsitColours.AutoMapper;
using UsitColours.Models;

namespace UsitColours.Areas.Profile.Models
{
    public class FlightStatisticViewModel: IMapFrom<Flight>
    {
        public string AirportDepartureName { get; set; }

        public string AirportArrivalName { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public DateTime DateOfArrival { get; set; }

        public decimal Price { get; set; }
    }
}