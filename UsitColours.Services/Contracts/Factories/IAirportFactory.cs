using UsitColours.Models;

namespace UsitColours.Services.Contracts.Factories
{
    public interface IAirportFactory
    {
        Airport CreateAirport(string name, int cityId);
    }
}


