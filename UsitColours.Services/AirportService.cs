using System;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Services
{
    public class AirportService : IAirportService
    {
        private IUsitData usitData;

        public AirportService(IUsitData usitData)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            this.usitData = usitData;
        }

        public IEnumerable<Airport> GetAllAirportsInCity(int cityId)
        {
            return this.usitData.Airports.All
                    .Where(a => a.CityId == cityId)
                    .OrderBy(a => a.Name)
                    .ToList();
        }

        public void AddAirport(Airport airport)
        {
            this.usitData.Airports.Add(airport);
            this.usitData.SaveChanges();
        }
    }
}

