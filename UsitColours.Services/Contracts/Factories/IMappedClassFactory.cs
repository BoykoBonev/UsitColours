using System;
using UsitColours.Services.Models;

namespace UsitColours.Services.Contracts.Factories
{
    public interface IMappedClassFactory
    {
        PresentationFlight CreatePresentationFlight(int id, string cityDepartureName, string cityArivalName, string airportDepartureName, string airportArivalName, DateTime departureDate, DateTime arivalDate, decimal price, string airlineName);

        MappedFlight CreateMappedFlight(int id, SecondFlightNode deparuteAirport, SecondFlightNode arrivalAirport, DateTime departureTime, DateTime arrivalTime);

        SecondFlightNode CreateSecondFlightNode(int airportId);
    }
}
