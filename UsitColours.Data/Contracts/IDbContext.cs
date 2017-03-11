using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using UsitColours.Models;

namespace UsitColours.Data.Contracts
{
    public interface IDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        DbSet<Airline> Airlines { get; set; }

        DbSet<Airport> Airports { get; set; }

        DbSet<City> Cities { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Flight> Flights { get; set; }

        DbSet<Job> Jobs { get; set; }

        void SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
