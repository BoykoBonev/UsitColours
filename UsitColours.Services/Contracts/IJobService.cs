using System;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Services.Contracts
{
    public interface IJobService
    {
        void AddJob(string jobTitle, string jobDescription, int slots, DateTime startDate, DateTime endDate, decimal wage, string companyName, decimal price, int cityId, string imagePath);

        void AddJob(Job job);

        IEnumerable<Job> GetAllJobs();
        IEnumerable<Job> GetAllJobsFromByTerm(string searchedTerm);
        Job GetJobById(int id);
        IEnumerable<Job> GetSoonestJobs();
    }
}