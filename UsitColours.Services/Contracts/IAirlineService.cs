using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface IAirlineService
    {
        void AddAirline(string name);
        IEnumerable<Airline> GetAllAirlines();
    }
}