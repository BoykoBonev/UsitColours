using UsitColours.Models;

namespace UsitColours.Services.Contracts.Factories
{
    public interface IAirlineFactory
    {
        Airline CreateAirline(string name);
    }
}
