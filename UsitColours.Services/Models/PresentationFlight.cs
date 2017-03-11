using System;

namespace UsitColours.Services.Models
{
    public class PresentationFlight
    {
        public PresentationFlight(int id, string cityDepartureName, string cityArivalName, string airportDepartureName, string airportArivalName, DateTime departureDate, DateTime arivalDate, decimal price, string airlineName)
        {
            this.Id = id;
            this.CityDepartureName = cityDepartureName;
            this.CityArivalName = cityArivalName;
            this.AirportDepartureName = airportDepartureName;
            this.AirportArivalName = airportArivalName;
            this.DepartureDate = departureDate;
            this.ArivalDate = arivalDate;
            this.Price = price;
            this.AirlineName = airlineName;
        }

        public int Id { get; set; }

        public string CityDepartureName { get; set; }

        public string CityArivalName { get; set; }

        public string AirportDepartureName { get; set; }

        public string AirportArivalName { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArivalDate { get; set; }

        public decimal Price { get; set; }

        public string AirlineName { get; set; }
    }
}
