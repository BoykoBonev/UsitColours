using System;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Services
{
    public class AirportService
    {
        private readonly IAirportFactory airportFactory;
        private IUsitData usitData;

        public AirportService(IUsitData usitData, IAirportFactory airportFactory)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            if (airportFactory == null)
            {
                throw new NullReferenceException("AirportFactory");
            }

            this.usitData = usitData;
            this.airportFactory = airportFactory;
        }

        public IEnumerable<Airport> GetAllAirportsInCity(int cityId)
        {
            return this.usitData.Airports.All
                    .Where(a => a.CityId == cityId)
                    .OrderBy(a => a.Name)
                    .ToList();
        }

        public void AddAirport(int cityId, string name)
        {
            Airport airport = this.airportFactory.CreateAirport(name, cityId);

            this.usitData.Airports.Add(airport);
            this.usitData.SaveChanges();
        }
    }
}

