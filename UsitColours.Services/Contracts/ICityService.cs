using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface ICityService
    {
        void AddCity(City city);


        IEnumerable<City> GetCityInCountry(int countryId);
    }
}