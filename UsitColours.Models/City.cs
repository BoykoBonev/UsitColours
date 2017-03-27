using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UsitColours.Models.Constants;

namespace UsitColours.Models
{
    public class City
    {
        private ICollection<Airport> airports;
        private ICollection<Job> jobs;

        public City()
        {
            this.Jobs = new HashSet<Job>();
            this.airports = new HashSet<Airport>();
        }
        public City(string name, int countryId)
            : this()
        {
            this.Name = name;
            this.CountryId = countryId;
        }

        [MinLength(ModelConstants.CityMinLength)]
        [MaxLength(ModelConstants.CityMaxLength)]
        public string Name { get; set; }

        public int Id { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Airport> Airports
        {
            get
            {
                return this.airports;
            }

            set
            {
                this.airports = value;
            }
        }

        public virtual ICollection<Job> Jobs
        {
            get
            {
                return this.jobs;
            }

            set
            {
                this.jobs = value;
            }
        }
    }
}
