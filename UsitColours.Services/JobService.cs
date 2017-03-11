using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts.Factories;
using UsitColours.Services.Utils;

namespace UsitColours.Services
{
    public class JobService
    {
        private readonly IUsitData usitData;
        private readonly IJobFactory jobFactory;
        public JobService(IUsitData usitData, IJobFactory jobFactory)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            if(jobFactory == null)
            {
                throw new NullReferenceException("JobFactory");
            }

            this.usitData = usitData;
            this.jobFactory = jobFactory;
        }

        public void AddJob(string jobTitle, string jobDescription, int slots, DateTime startDate, DateTime endDate, decimal wage, string companyName, decimal price, int cityId, string imagePath)
        {
            Job job = this.jobFactory.CreateJob(cityId, jobTitle, jobDescription, slots, startDate, endDate, wage, companyName, price, imagePath);

            this.usitData.Jobs.Add(job);
            this.usitData.SaveChanges();
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return this.usitData.Jobs.All.ToList();
        }

        public IEnumerable<Job> GetAllJobsFromByTerm(string searchedTerm)
        {
            return this.usitData.Jobs.All
                .Where(j => j.JobTitle.Contains(searchedTerm) || j.JobDescription.Contains(searchedTerm) || j.CompanyName.Contains(searchedTerm))
                .OrderBy(j => j.JobTitle)
                .Include(j => j.City)
                .ToList();
        }

        public Job GetJobById(int id)
        {
            return this.usitData.Jobs.GetById(id);
        }

        public IEnumerable<Job> GetSoonestJobs()
        {
            var take = 3;
            var date = TimeProvider.Current.GetDate();

            var jobs = this.usitData.Jobs.All.Where(j => j.StartDate > date && j.Slots > 0)
                .OrderBy(j => j.StartDate)
                .Take(take);

            return jobs.ToList();
        }
    }
}
