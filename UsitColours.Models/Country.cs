using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsitColours.Models
{
    public class Country
    {
        private ICollection<City> cities;

        public Country()
        {
            this.cities = new HashSet<City>();
        }

        public Country(string name)
            : this()
        {
            this.Name = name;
        }

        public int Id { get; set; }

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
    }
}
