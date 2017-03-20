using System;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Services
{
    public class CityService : ICityService
    {
        private readonly IUsitData usitData;

        public CityService(IUsitData usitData)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            this.usitData = usitData;
        }

        public IEnumerable<City> GetCityInCountry(int countryId)
        {
            var cities = this.usitData.Cities.All
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.Name)
                .ToList();

            return cities;
        }

        public void AddCity(City city)
        {

            this.usitData.Cities.Add(city);

            this.usitData.SaveChanges();
        }
    }
}
