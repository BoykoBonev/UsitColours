using UsitColours.Models;

namespace UsitColours.Services.Contracts.Factories
{
    public interface ILocationFactory
    {
        Country CreateCountry(string name);

        City CreateCity(string name, int countryId);
    }
}
