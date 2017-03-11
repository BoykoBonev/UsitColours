using System;
using System.Linq;
using UsitColours.Models;
using UsitColours.Data.Contracts;
using System.Data.Entity;
using System.Collections.Generic;

namespace UsitColours.Services
{
    public class TicketService
    {
        private readonly IUsitData usitData;

        public TicketService(IUsitData usitData)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            this.usitData = usitData;
        }

        public IEnumerable<Ticket> GetTicketSales(DateTime startPeriod, DateTime endPeriod)
        {
            var tickets = this.usitData.Tickets.All
                .Where(t => startPeriod <= t.BoughtDate && t.BoughtDate <= endPeriod)
                .Include(t => t.ApplicationUser)
                .ToList();

            return tickets;
        }
    }
}
