using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UsitColours.Models
{
    public class SearchViewModel
    {
        public IEnumerable<SelectListItem> Countries { get; set; }

        public int DepartureAirportId { get; set; }

        public int ArivalAirportId { get; set; }

        public int FlightDate { get; set; }

        public int PassangerCount { get; set; }

        public int AirportDepartureId { get; set; }

        public int AirportArrivalId { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public int AvailableSeats { get; set; }
    }
}