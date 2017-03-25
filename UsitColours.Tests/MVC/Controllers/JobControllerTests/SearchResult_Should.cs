using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Models;

namespace UsitColours.Tests.MVC.Controllers.JobControllerTests
{
    [TestFixture]
    public class SearchResult_Should
    {
        [TestCase("dsada", 2)]
        [TestCase("21asd", 23)]
        [TestCase("aaaa", 1)]

        public void CallGetAllJobsFromByTermWithExpectedParameters(string searchTerm, int page)
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            jobService.Setup(j => j.GetAllJobsFromByTerm(searchTerm, page)).Returns(new JobSearchResult()
            {
                Count = 0,
                Jobs = new List<Job>()
            });

            //Act
            jobController.SearchResult(searchTerm, page);

            // Assert
            jobService.Verify(j => j.GetAllJobsFromByTerm(searchTerm, page), Times.Once);
        }

        [Test]
        public void CallMapOnMappingServiceWithExpectedJobObjects()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            var jobs = new List<Job>()
                {
                    new Job() {Id = 1 },
                    new Job() {Id = 2 },
                    new Job() {Id = 3 },
                };

            jobService.Setup(j => j.GetAllJobsFromByTerm(It.IsAny<string>(), It.IsAny<int>())).Returns(new JobSearchResult()
            {
                Count = 0,
                Jobs = jobs
            });

            //Act
            jobController.SearchResult(It.IsAny<string>(), It.IsAny<int>());

            // Assert
            foreach (var job in jobs)
            {
                mappingService.Verify(m => m.Map<JobBaseViewModel>(job), Times.Once);
            }
        }

        [TestCase(10, "asddsa")]
        [TestCase(22, "asddd")]
        public void Return_SearchJobResultPartialViewWithExpectedSearchJobViewModel(int jobCount, string searchTerm)
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            var jobs = new List<Job>()
                {
                    new Job() {Id = 1 },
                    new Job() {Id = 2 },
                    new Job() {Id = 3 },
                };

            jobService.Setup(j => j.GetAllJobsFromByTerm(It.IsAny<string>(), It.IsAny<int>())).Returns(new JobSearchResult()
            {
                Count = jobCount,
                Jobs = jobs
            });

            var jobViewModel = new JobBaseViewModel()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<JobBaseViewModel>(It.IsAny<Job>())).Returns(jobViewModel);

            // Act & Assert
            jobController
                .WithCallTo(h => h.SearchResult(searchTerm, jobCount))
                .ShouldRenderPartialView("_SearchJobResult")
              .WithModel<SearchJobViewModel>(viewModel =>
              {
                  Assert.AreEqual(jobCount, viewModel.Count);
                  Assert.AreEqual(searchTerm, viewModel.SearchTerm);
                  Assert.AreEqual(jobs.Count, viewModel.Jobs.Count());

                  foreach (var item in viewModel.Jobs)
                  {
                      Assert.AreEqual(1, item.Id);
                  }
              });
        }
    }
}

//public ActionResult SearchResult(string searchTerm, int page = 0)
//{

//    var jobsSearchResult = this.jobService.GetAllJobsFromByTerm(searchTerm, page);

//    var jobs = jobsSearchResult.Jobs;
//    var count = jobsSearchResult.Count;

//    var mappedJobs = jobs.Select(j => this.mappingService.Map<JobBaseViewModel>(j))
//        .ToList();

//    var searchViewModel = new SearchJobViewModel()
//    {
//        Jobs = mappedJobs,
//        Count = count,
//        SearchTerm = searchTerm
//    };

//    return PartialView("_SearchJobResult", searchViewModel);
//}