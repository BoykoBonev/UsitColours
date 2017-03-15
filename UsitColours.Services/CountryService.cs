using System;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUsitData usitData;
        private readonly ILocationFactory locationFactory;
        public CountryService(ILocationFactory locationFactory, IUsitData usitData)
        {
            if(usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            if(locationFactory == null)
            {
                throw new NullReferenceException("LocationFactory");
            }

            this.usitData = usitData;
            this.locationFactory = locationFactory;
        }

        public void AddCountry(string country)
        {
                Country newCountry = this.locationFactory.CreateCountry(country);

                this.usitData.Countries.Add(newCountry);

                this.usitData.SaveChanges();
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return this.usitData.Countries.All.ToList();
        }

        public void AddCountry(Country country)
        {
            this.usitData.Countries.Add(country);

            this.usitData.SaveChanges();
        }
    }
}
