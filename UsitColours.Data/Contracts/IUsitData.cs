using UsitColours.Data.Repositories;
using UsitColours.Models;

namespace UsitColours.Data.Contracts
{
    public interface IUsitData
    {
        IGenericRepository<Airline> Airlines { get; }

        IGenericRepository<Airport> Airports { get; }

        IGenericRepository<ApplicationUser> Users { get; }

        IGenericRepository<City> Cities { get; }

        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Flight> Flights { get; }

        IGenericRepository<Job> Jobs { get; }

        IGenericRepository<Ticket> Tickets { get; }

        void SaveChanges();
    }
}
