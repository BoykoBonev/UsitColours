using System;
using UsitColours.Models;

namespace UsitColours.Services.Contracts.Factories
{
    public interface IFlightFactory
    {
        Flight CreateFlight(int airlineId, int airportArrivalId, int airportDepartureId, DateTime arrivalDate, DateTime departureDate, decimal price, int availableSeats);
    }
}
