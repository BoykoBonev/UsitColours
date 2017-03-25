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

namespace UsitColours.Tests.MVC.Controllers.JobControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        [Test]
        public void ReturnHomeView_WhenIdIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            // Act and Assert
            jobController
                 .WithCallTo(h => h.Details(null))
                 .ShouldRenderView("Home");
        }

        [TestCase(1)]
        [TestCase(12)]
        [TestCase(131)]
        public void CallGetJobById_WithExpectedParsedId(int? id)
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            var jobId = (int)id;

            // Act
            jobController.Details(id);

            // Assert
            jobService.Verify(j => j.GetJobById(jobId), Times.Once);
        }

        [Test]
        public void CallMapOnMappingServiceOnceWithExpectedJob()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            int? jobId = 10;

            var job = new Job();

            jobService.Setup(j => j.GetJobById(It.IsAny<int>())).Returns(job);

            // Act
            jobController.Details(jobId);

            // Assert
            mappingService.Verify(m => m.Map<JobViewModel>(job), Times.Once);
        }

        [Test]
        public void RenderDefaultViewWithExpectedJobViewModel()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            int? jobId = 10;

            var job = new Job();

            jobService.Setup(j => j.GetJobById(It.IsAny<int>())).Returns(job);

            var jobViewModelId = 10;
            var jobViewModel = new JobViewModel()
            {
                Id = jobViewModelId
            };

            mappingService.Setup(m => m.Map<JobViewModel>(job)).Returns(jobViewModel);

            // Act & Assert
            jobController
                .WithCallTo(h => h.Details(jobId))
                .ShouldRenderDefaultView()
              .WithModel<JobViewModel>(viewModel =>
              {
                  Assert.AreEqual(jobViewModel, viewModel);
                  Assert.AreEqual(jobViewModelId, viewModel.Id);
              });
        }
    }
}
