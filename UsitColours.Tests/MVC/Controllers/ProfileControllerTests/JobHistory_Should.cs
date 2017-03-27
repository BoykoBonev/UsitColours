using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Profile.Controllers;
using UsitColours.Areas.Profile.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class JobHistory_Should
    {
        [TestCase("User1")]
        [TestCase("User2")]

        public void CallGetJobHistoryOnUserSeriveWithProvidedUserId(string userId)
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();
            var profileController = new ProfileController(mockedMappingService.Object, mockedUserService.Object);

            profileController.GetLoggedUserId = () => userId;

            // Act
            profileController.JobHistory();

            // Assert
            mockedUserService.Verify(u => u.GetJobsHistory(userId), Times.Once);
        }

        [Test]
        public void CallMapOnMappingServiceOnProvidedFlightCollection()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();
            var profileController = new ProfileController(mockedMappingService.Object, mockedUserService.Object);

            var jobs = new List<Job>()
            {
                new Job() {Id = 1 },
                new Job() {Id = 2 },
            };

            mockedUserService.Setup(u => u.GetJobsHistory(It.IsAny<string>())).Returns(jobs);

            profileController.GetLoggedUserId = () => It.IsAny<string>();


            // Act
            profileController.JobHistory();

            // Assert

            foreach (var job in jobs)
            {
                mockedMappingService.Verify(m => m.Map<JobStatisticViewModel>(job), Times.Once);
            }
        }

        [Test]
        public void ReturnPartialView__JobList_WithProvidedVIewModel()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();
            var profileController = new ProfileController(mockedMappingService.Object, mockedUserService.Object);

            var jobs = new List<Job>()
            {
                new Job() {Id = 1 },
                new Job() {Id = 2 },
            };

            mockedUserService.Setup(u => u.GetJobsHistory(It.IsAny<string>())).Returns(jobs);

            profileController.GetLoggedUserId = () => It.IsAny<string>();

            var jobResultMap = new JobStatisticViewModel()
            {
                Price = 1
            };

            mockedMappingService.Setup(m => m.Map<JobStatisticViewModel>(It.IsAny<Job>())).Returns(jobResultMap);

            // Act and Assert
            profileController
               .WithCallTo(h => h.JobHistory())
               .ShouldRenderPartialView("_JobList")
             .WithModel<IEnumerable<JobStatisticViewModel>>(viewModel =>
             {
                 Assert.AreEqual(jobs.Count, viewModel.Count());

                 foreach (var item in viewModel)
                 {
                     Assert.AreEqual(1, item.Price);
                 }
             });
        }
    }
}
