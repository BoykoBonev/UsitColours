using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using UsitColours.AutoMapper;
using UsitColours.Models;

namespace UsitColours.Areas.Admin.Models
{
    public class AddFlightViewModel
    {
        public List<SelectListItem> Countries { get; set; }

        public List<SelectListItem> Airlines { get; set; }

        public int AirportArrivalId { get; set; }

        public int AirportDepartureId { get; set; }


        public int AirlineId { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public DateTime DateOfArrival { get; set; }

        public int Price { get; set; }

        public int AvailableSeats { get; set; }
    }
}