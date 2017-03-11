using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsitColours.Models
{
    public class Job
    {
        private ICollection<ApplicationUser> users;

        public Job()
        {
            this.users = new HashSet<ApplicationUser>();
        }

        public Job(int cityId, string jobTitle, string jobDescription, int slots,
            DateTime startDate, DateTime endDate, decimal wage, string companyName, decimal price, string imagePath)
            : this()
        {
            this.CityId = cityId;
            this.JobTitle = jobTitle;
            this.JobDescription = jobDescription;
            this.Slots = slots;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Wage = wage;
            this.CompanyName = companyName;
            this.Price = price;
            this.ImagePath = imagePath;
        }

        public int Id { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public int Slots { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Wage { get; set; }

        public string CompanyName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<ApplicationUser> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
            }
        }

    }
}
