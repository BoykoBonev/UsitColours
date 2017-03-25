using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Contracts.Factories;
using UsitColours.Services.Models;
using UsitColours.Services.Utils;

namespace UsitColours.Services
{
    public class JobService : IJobService
    {
        private readonly IUsitData usitData;

        public JobService(IUsitData usitData)
        {
            if (usitData == null)
            {
                throw new NullReferenceException("UsitData");
            }

            this.usitData = usitData;
        }

        public void AddJob(Job job)
        {
            this.usitData.Jobs.Add(job);
            this.usitData.SaveChanges();
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return this.usitData.Jobs.All.ToList();
        }

        public JobSearchResult GetAllJobsFromByTerm(string searchedTerm, int page)
        {
            int count = this.usitData.Jobs.All
                .Where(j => j.JobTitle.Contains(searchedTerm) || j.JobDescription.Contains(searchedTerm) || j.CompanyName.Contains(searchedTerm))
                .Count();

            int pageSize = 4;

            if (count % pageSize > 0)
            {
                count = count / pageSize + 1;
            }
            else
            {
                count = count / pageSize;
            }

            int skip = page * pageSize;
            if (page > 0)
            {
                page = (page - 1) * pageSize;
            }

            var jobs = this.usitData.Jobs.All
                .Where(j => j.JobTitle.Contains(searchedTerm) || j.JobDescription.Contains(searchedTerm) || j.CompanyName.Contains(searchedTerm))
                .OrderBy(j => j.JobTitle)
                .Include(j => j.City)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            JobSearchResult result = new JobSearchResult()
            {
                Jobs = jobs,
                Count = count
            };

            return result;
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
                .Take(take)
                .Include(j => j.City);

            return jobs.ToList();
        }
    }
}
