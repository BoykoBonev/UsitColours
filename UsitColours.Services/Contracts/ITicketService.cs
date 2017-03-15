using System;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface ITicketService
    {
        IEnumerable<Ticket> GetTicketSales(DateTime startPeriod, DateTime endPeriod);
    }
}