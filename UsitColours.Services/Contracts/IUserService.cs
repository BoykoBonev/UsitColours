using System.Collections.Generic;
using UsitColours.Models;
using UsitColours.Services.Models;

namespace UsitColours.Services.Contracts
{
    public interface IUserService
    {
        bool AttachJobToUser(string userId, int jobId);

        bool BuyTicket(string userId, IEnumerable<PresentationFlight> flights);

        IEnumerable<Flight> GetFlightHistory(string userId);

        IEnumerable<Job> GetJobsHistory(string userId);

        IEnumerable<Flight> GetUpcommingFlights(string userId);

        IEnumerable<Job> GetUpcommingJobs(string userId);
    }
}