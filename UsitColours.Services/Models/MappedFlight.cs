using System;

namespace UsitColours.Services.Models
{
    public class MappedFlight: IComparable
    {
        public MappedFlight(int id, SecondFlightNode deparuteAirport, SecondFlightNode arrivalAirport, DateTime departureTime, DateTime arrivalTime)
        {
            this.Id = id;
            this.DepartureAirport = deparuteAirport;
            this.ArrivalAirport = arrivalAirport;
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
        }

        public int Id { get; set; }

        public SecondFlightNode DepartureAirport { get; set; }

        public SecondFlightNode ArrivalAirport { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
