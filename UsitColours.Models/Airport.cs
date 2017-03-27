using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UsitColours.Models.Constants;

namespace UsitColours.Models
{
    public class Airport
    {
        private ICollection<Flight> departures;
        private ICollection<Flight> arrivals;

        public Airport()
        {
            this.departures = new HashSet<Flight>();
            this.arrivals = new HashSet<Flight>();
        }

        public Airport(string name, int cityId)
            :this()
        {
            this.Name = name;
            this.CityId = cityId;
        }

        public int Id { get; set; }

        [MinLength(ModelConstants.AirportMinLength)]
        [MaxLength(ModelConstants.AirportMaxLength)]
        public string Name { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Flight> Departure
        {
            get
            {
                return this.departures;
            }

            set
            {
                this.departures = value;
            }
        }

        public virtual ICollection<Flight> Arrival
        {
            get
            {
                return this.arrivals;
            }

            set
            {
                this.arrivals = value;
            }
        }
    }
}
