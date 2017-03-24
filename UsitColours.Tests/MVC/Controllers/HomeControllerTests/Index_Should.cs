using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void Call()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();
            var homeController = new HomeController(flightService.Object, mappingService.Object, jobService.Object);

            var flightVIewModel = new FlightVIewModel()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<FlightVIewModel>(It.IsAny<Flight>())).Returns(flightVIewModel);

            var jobVIewModel = new JobViewModel()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<JobViewModel>(It.IsAny<Job>())).Returns(jobVIewModel);

            var flightsCollection = new List<Flight>()
            {
                new Flight(),
                new Flight(),
                new Flight()
            };

            flightService.Setup(f => f.GetCheapestFlights()).Returns(flightsCollection);


            var jobCollection = new List<Job>()
            {
                new Job(),
                new Job(),
                new Job()
            };

            jobService.Setup(f => f.GetSoonestJobs()).Returns(jobCollection);

            // Act & Assert
            homeController
                .WithCallTo(h => h.Index())
                .ShouldRenderDefaultView()
              .WithModel<HomeViewModel>(viewModel =>
              {
                  Assert.AreEqual(3, viewModel.Jobs.Count());
                  Assert.AreEqual(3, viewModel.Flights.Count());
              });
        }

        [Test]
        public void CallGetCheapestFlightsOnce()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();
            var homeController = new HomeController(flightService.Object, mappingService.Object, jobService.Object);

            // Act
            homeController.Index();

            // Assert
            flightService.Verify(f => f.GetCheapestFlights(), Times.Once);
        }

        [Test]
        public void CallGetSoonestJobsOnce()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();
            var homeController = new HomeController(flightService.Object, mappingService.Object, jobService.Object);

            // Act
            homeController.Index();

            // Assert
            jobService.Verify(j => j.GetSoonestJobs(), Times.Once);
        }




    }
}
