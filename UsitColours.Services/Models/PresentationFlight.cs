using System;

namespace UsitColours.Services.Models
{
    public class PresentationFlight
    {
        public PresentationFlight(int id, string cityDepartureName, string cityArivalName, string airportDepartureName, string airportArivalName, DateTime departureDate, DateTime arivalDate, decimal price, string airlineName, int availableSeats)
        {
            this.Id = id;
            this.CityDepartureName = cityDepartureName;
            this.CityArivalName = cityArivalName;
            this.AirportDepartureName = airportDepartureName;
            this.AirportArrivalName = airportArivalName;
            this.DateOfDeparture = departureDate;
            this.DateOfArrival = arivalDate;
            this.Price = price;
            this.AirlineName = airlineName;
            this.AvailableSeats = availableSeats;
        }

        public int Id { get; set; }

        public string CityDepartureName { get; set; }

        public string CityArivalName { get; set; }

        public string AirportDepartureName { get; set; }

        public string AirportArrivalName { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public DateTime DateOfArrival { get; set; }

        public decimal Price { get; set; }

        public string AirlineName { get; set; }

        public int AvailableSeats { get; set; }
    }
}
