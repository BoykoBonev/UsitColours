using System;
using System.Collections.Generic;
using UsitColours.Models;
using UsitColours.Services.Models;

namespace UsitColours.Services.Contracts
{
    public interface IFlightService
    {
      //  IEnumerable<Flight> FilterFlights(string type, string filterExpression);

        IEnumerable<Flight> GetCheapestFlights();

        Flight GetDetailedFlight(int id);

        IEnumerable<PresentationFlight> GetFlights(int currentAirportId, int destinationAirportId, DateTime travelDate, int count);

     //   void UpdateFlight(Flight flight);

        void AddFlight(Flight flight);

    }
}