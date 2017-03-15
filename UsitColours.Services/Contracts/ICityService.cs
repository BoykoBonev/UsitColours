using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface ICityService
    {
        void AddCity(int countryId, string city);

        void AddCity(City city);

        IEnumerable<City> GetAllCities();
        IEnumerable<City> GetCityInCountry(int countryId);
    }
}