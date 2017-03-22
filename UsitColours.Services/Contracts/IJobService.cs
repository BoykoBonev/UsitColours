using System;
using System.Collections.Generic;
using UsitColours.Models;
using UsitColours.Services.Models;

namespace UsitColours.Services.Contracts
{
    public interface IJobService
    {
        void AddJob(Job job);

        IEnumerable<Job> GetAllJobs();

        JobSearchResult GetAllJobsFromByTerm(string searchedTerm, int page);

        Job GetJobById(int id);

        IEnumerable<Job> GetSoonestJobs();
    }
}