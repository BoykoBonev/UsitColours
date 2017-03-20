using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface IAirportService
    {
        IEnumerable<Airport> GetAllAirportsInCity(int cityId);

        void AddAirport(Airport airport);
    }
}