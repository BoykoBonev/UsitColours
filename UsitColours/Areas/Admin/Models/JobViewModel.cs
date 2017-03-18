using System;
using AutoMapper;
using UsitColours.AutoMapper;
using UsitColours.Models;

namespace UsitColours.Areas.Admin.Models
{
    public class JobViewModel: IHaveCustomMappings
    {
        public int CityId { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public int Slots { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Wage { get; set; }

        public string CompanyName { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public bool IsDefaultImage { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<JobViewModel, Job>()
            .ConstructUsing(x => new Job(x.CityId, x.JobTitle, x.JobDescription, x.Slots, x.StartDate, x.EndDate, x.Wage, x.CompanyName, x.Price, x.ImagePath));
        }
    }
}