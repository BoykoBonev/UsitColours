using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface IAirportService
    {
        void AddAirport(int cityId, string name);
        IEnumerable<Airport> GetAllAirportsInCity(int cityId);
    }
}