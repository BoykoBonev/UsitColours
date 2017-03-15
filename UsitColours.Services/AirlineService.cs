using System;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineFactory airlineFactory;
        private IUsitData usitData;
        public AirlineService(IUsitData usitData, IAirlineFactory airlineFactory)
        {
            if(usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            if(airlineFactory == null)
            {
                throw new NullReferenceException("AirlineFactory");
            }

            this.usitData = usitData;
        }

        public IEnumerable<Airline> GetAllAirlines()
        {
            return this.usitData.Airlines.All.ToList();
        }

        public void AddAirline(string name)
        {
            Airline airline = this.airlineFactory.CreateAirline(name);

            this.usitData.Airlines.Add(airline);
            this.usitData.SaveChanges();
        }
    }
}
