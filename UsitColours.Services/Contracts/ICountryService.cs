using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface ICountryService
    {
        void AddCountry(string country);

        void AddCountry(Country country);

        IEnumerable<Country> GetAllCountries();
    }
}