using System;
using UsitColours.Models;

namespace UsitColours.Services.Contracts.Factories
{
    public interface ITicketFactory
    {
        Ticket CreateTicket(int flightId, string applicationUserId, DateTime boughtDate);
    }
}
