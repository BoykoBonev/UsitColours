using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UsitColours.Models.Constants;

namespace UsitColours.Models
{
    public class Airline
    {
        private ICollection<City> cities;
        private ICollection<Flight> flights;

        public Airline()
        {
            this.cities = new HashSet<City>();
            this.flights = new HashSet<Flight>();
        }

        public Airline(string name)
            : this()
        {
            this.Name = name;
        }

        public int Id { get; set; }

        [MinLength(ModelConstants.AirlineMinLength)]
        [MaxLength(ModelConstants.AirlineMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities
        {
            get
            {
                return this.cities;
            }

            set
            {
                this.cities = value;
            }
        }

        public virtual ICollection<Flight> Flights
        {
            get
            {
                return this.flights;
            }

            set
            {
                this.flights = value;
            }
        }
    }
}
