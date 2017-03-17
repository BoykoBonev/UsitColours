using System;
using UsitColours.AutoMapper;

namespace UsitColours.Models
{

    public class DetailsFlightViewModel : IMapFrom<Flight>
    {
        public int Id { get; set; }

        public string AirportDepartureName { get; set; }

        public string AirportArrivalName { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public DateTime DateOfArrival { get; set; }

        public decimal Price { get; set; }

        public int AvailableSeats { get; set; }

        public string AirlineName { get; set; }
    }
}