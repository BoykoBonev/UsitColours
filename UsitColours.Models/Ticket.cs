using System;

namespace UsitColours.Models
{
    public class Ticket
    {
        public Ticket()
        {
        }

        public Ticket(int flightId, string applicationUserId, DateTime boughtDate)
            : base()
        {
            this.FlightId = flightId;
            this.ApplicationUserId = Guid.Parse(applicationUserId);
            this.BoughtDate = boughtDate;
        }

        public int Id { get; set; }

        public int FlightId { get; set; }

        public virtual Flight Flight { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime BoughtDate { get; set; }
    }
}
