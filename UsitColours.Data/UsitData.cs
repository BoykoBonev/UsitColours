using System;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;

namespace UsitColours.Data
{
    public class UsitData : IUsitData
    {
        private IDbContext dbContext;

        public UsitData(IDbContext dbContext)
        {
            if(dbContext == null)
            {
                throw new NullReferenceException("DbContext");
            }

            this.dbContext = dbContext;
        }

        public IGenericRepository<Airline> Airlines
        {
            get
            {
                return this.GetRepository<Airline>();
            }
        }

        public IGenericRepository<Airport> Airports
        {
            get
            {
                return this.GetRepository<Airport>();
            }
        }

        public IGenericRepository<City> Cities
        {
            get
            {
                return this.GetRepository<City>();
            }
        }

        public IGenericRepository<Country> Countries
        {
            get
            {
                return this.GetRepository<Country>();
            }
        }

        public IGenericRepository<Flight> Flights
        {
            get
            {
                return this.GetRepository<Flight>();
            }
        }

        public IGenericRepository<Job> Jobs
        {
            get
            {
                return this.GetRepository<Job>();
            }
        }

        public IGenericRepository<Ticket> Tickets
        {
            get
            {
                return this.GetRepository<Ticket>();
            }
        }

        public IGenericRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public void SaveChanges()
        {
           this.dbContext.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>()
         where T : class
        {
            return (IGenericRepository<T>)(Activator.CreateInstance(typeof(T), this.dbContext));
        }
    }
}
