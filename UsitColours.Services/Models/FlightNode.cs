using System;

namespace UsitColours.Services.Models
{
    public class FlightNode
    {
        public int FlightId { get; set; }

        public string ArrivalCity { get; set; }

        public string DeparureCity { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public decimal Price { get; set; }
    }
}
