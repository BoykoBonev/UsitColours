//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using UsitColours.Data.Contracts;
//using UsitColours.Data.Repositories;
//using UsitColours.Models;
//using UsitColours.Services;

//namespace UsitColours.Tests.Services.JobServiceTests
//{
//    [TestFixture]
//    public class GetAllJobsFromByTerm_Should
//    {
//        [TestCase("first")]
//        [TestCase("second")]
//        [TestCase("third")]
//        public void ReturnExpectedCollectionOfJobs_WhenSearchTermIsPassed(string searchTerm)
//        {
//            // Arrange
//            var mockedData = new Mock<IUsitData>();
//            var jobService = new JobService(mockedData.Object);

//            var mockedJobRepository = new Mock<IGenericRepository<Job>>();

//            mockedData.Setup(d => d.Jobs).Returns(mockedJobRepository.Object);

//            var jobs = new List<Job>()
//            {
//                new Job() {Id = 1, JobTitle = searchTerm, JobDescription = "Default",  CompanyName =  "Default1", City = new City()},
//                new Job() {Id = 2, JobTitle =  "Default", JobDescription =  "Default1",  CompanyName =  "Default3", City = new City()},
//                new Job() {Id = 3, JobTitle = searchTerm, JobDescription =  "Default",  CompanyName =  "Default1", City = new City()},
//                new Job() {Id = 4, JobTitle = searchTerm, JobDescription = searchTerm,  CompanyName =  "Default1", City = new City()},
//                new Job() {Id = 5, JobTitle = searchTerm, JobDescription = "Defaul3t",  CompanyName = searchTerm, City = new City()},
//                new Job() {Id = 6, JobTitle = searchTerm, JobDescription =  "Default31",  CompanyName =  "Default", City = new City() { Name = "Tupo" } },
//            }.AsQueryable();

//            mockedJobRepository.Setup(r => r.All).Returns(jobs);

//            var expectedJobs = jobs
//                .Where(j => j.JobTitle.Contains(searchTerm) || j.JobDescription.Contains(searchTerm) || j.CompanyName.Contains(searchTerm))
//                .OrderBy(j => j.JobTitle)
//                .Include(j => j.City)
//                .Skip(skip)
//                .Take(pageSize)
//                .ToList();

//            // Act
//            var actualJobs = jobService.GetAllJobsFromByTerm(searchTerm);

//            // Assert
//            CollectionAssert.AreEqual(expectedJobs, actualJobs);
//        }
//    }
//}