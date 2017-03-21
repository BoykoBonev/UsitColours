using System.Collections.Generic;

namespace UsitColours.Models
{
    public class HomeViewModel
    {
        public IEnumerable<FlightVIewModel> Flights { get; set; }

        public IEnumerable<JobViewModel> Jobs { get; set; }

    }
}