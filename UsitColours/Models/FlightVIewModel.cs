using System;
using UsitColours.AutoMapper;

namespace UsitColours.Models
{
    public class FlightVIewModel: IMapFrom<Flight>
    {
        public int Id { get; set; }

        public string AirportDepartureName { get; set; }

        public string AirportArrivalName { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public decimal Price { get; set; }
    }
}


