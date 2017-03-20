using System;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface IJobService
    {
        void AddJob(Job job);

        IEnumerable<Job> GetAllJobs();

        IEnumerable<Job> GetAllJobsFromByTerm(string searchedTerm);

        Job GetJobById(int id);

        IEnumerable<Job> GetSoonestJobs();
    }
}