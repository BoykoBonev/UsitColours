using System;
using System.Linq;
using System.Collections.Generic;
using UsitColours.Models;
using UsitColours.Services.Contracts.Factories;
using UsitColours.Services.Models;
using UsitColours.Services.Utils;
using UsitColours.Data.Contracts;
using UsitColours.Services.Contracts;

namespace UsitColours.Services
{
    public class UserService : IUserService
    {
        private readonly IUsitData usitData;
        private readonly ITicketFactory ticketFactory;

        public UserService(IUsitData usitData, ITicketFactory ticketFactory)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            if (ticketFactory == null)
            {
                throw new NullReferenceException("TicketFactory");
            }

            this.ticketFactory = ticketFactory;
            this.usitData = usitData;
        }

        public bool BuyTicket(string userId, IEnumerable<PresentationFlight> flights)
        {
            var user = this.usitData.Users.GetById(userId);

            if (user.Money < flights.Sum(f => f.Price))
            {
                return false;
            }

            foreach (var flight in flights)
            {
                var ticket = this.ticketFactory.CreateTicket(flight.Id, userId, TimeProvider.Current.GetDate());
                user.Money -= flight.Price;

                var flightToReduceSeat = this.usitData.Flights.GetById(flight.Id);
                flightToReduceSeat.AvailableSeats -= 1;

                user.Tickets.Add(ticket);

                this.usitData.Tickets.Add(ticket);
                this.usitData.Flights.Update(flightToReduceSeat);
            }

            this.usitData.Users.Update(user);

            this.usitData.SaveChanges();

            return true;
        }

        public IEnumerable<Flight> GetFlightHistory(string userId)
        {
            var currentDate = TimeProvider.Current.GetDate();

            var flights = this.usitData.Users.All.Where(u => u.Id == userId).SelectMany(p => p.Tickets)
                .Select(t => t.Flight)
                .Where(f => f.DateOfDeparture < currentDate)
                .OrderBy(t => t.DateOfDeparture)
                .ToList();

            return flights.ToList();
        }

        public IEnumerable<Flight> GetUpcommingFlights(string userId)
        {
            var currentDate = TimeProvider.Current.GetDate();

            var flights = this.usitData.Users.All
                .Where(u => u.Id == userId).SelectMany(p => p.Tickets)
                .Select(t => t.Flight).Where(f => f.DateOfDeparture > currentDate)
                .OrderBy(t => t.DateOfDeparture).ToList();

            return flights;
        }

        public bool AttachJobToUser(string userId, int jobId)
        {
            var currentUser = this.usitData.Users.GetById(userId);

            var job = this.usitData.Jobs.GetById(jobId);

            if(currentUser.Money < job.Price)
            {
                return false;
            }

            currentUser.Jobs.Add(job);
            this.usitData.Users.Update(currentUser);
            this.usitData.SaveChanges();
            return true;
        }

        public IEnumerable<Job> GetUpcommingJobs(string userId)
        {
            var currentDate = TimeProvider.Current.GetDate();

            var jobs = this.usitData.Users.All
                .Where(u => u.Id == userId)
                .SelectMany(p => p.Jobs)
                .Where(f => f.StartDate > currentDate)
                .OrderBy(t => t.StartDate)
                .ToList();

            return jobs;
        }

        public IEnumerable<Job> GetJobsHistory(string userId)
        {
            var jobs = this.usitData.Users.All
                .Where(u => u.Id == userId)
                .SelectMany(p => p.Jobs)
                .OrderBy(t => t.StartDate)
                .ToList();

            return jobs;
        }
    }
}
