using System;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Services
{
    public class CityService
    {
        private readonly IUsitData usitData;
        private readonly ILocationFactory locationFactory;

        public CityService(IUsitData usitData, ILocationFactory locationFactory)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            if (locationFactory == null)
            {
                throw new NullReferenceException("LocationFactory");
            }

            this.usitData = usitData;
            this.locationFactory = locationFactory;
        }

        public void AddCity(int countryId, string city)
        {
           
                City newCity = this.locationFactory.CreateCity(city, countryId);

                this.usitData.Cities.Add(newCity);

                this.usitData.SaveChanges();
        }

        public IEnumerable<City> GetCityInCountry(int countryId)
        {
            var cities = this.usitData.Cities.All
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.Name)
                .ToList();

            return cities;
        }

        public IEnumerable<City> GetAllCities()
        {
            return this.usitData.Cities.All.ToList();
        }
    }
}
