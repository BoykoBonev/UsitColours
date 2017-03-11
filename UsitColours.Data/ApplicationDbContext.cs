using Microsoft.AspNet.Identity.EntityFramework;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using System.Data.Entity;

namespace UsitColours.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<Airport> Airports { get; set; }

        public virtual DbSet<Airline> Airlines { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Flight> Flights { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        void IDbContext.SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>()
                .HasMany(x => x.Arrival)
                .WithRequired(x => x.AirportArrival)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Airport>()
               .HasMany(x => x.Arrival)
               .WithRequired(x => x.AirportDeparture)
               .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}