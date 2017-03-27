using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Controllers;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.AdminControllerTests
{
    [TestFixture]
    public class AddFlightPost_Should
    {
        [Test]
        public void RedirectToIndexAction()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();
            var adminController = new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object);

            adminController.ModelState.AddModelError(string.Empty, new Exception());

            var flight = new FlightModel();

            // Act and Assert
            adminController.WithCallTo(a => a.AddFlight(flight))
                .ShouldRedirectTo(typeof(AdminController).GetMethod("Index"));
        }

        [Test]
        public void CallMapOnMappingServiceWithExpectedFlight()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();
            var adminController = new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object);

            var flight = new FlightModel();

            // Act
            adminController.AddFlight(flight);

            // Assert
            mappingService.Verify(m => m.Map<Flight>(flight), Times.Once);
        }

        [Test]
        public void CallAddFlightOnFlightServiceWIthExpectedFlight()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();
            var adminController = new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object);

            var flight = new FlightModel();

            var expectedFlight = new Flight() { Id = 1 };
            mappingService.Setup(m => m.Map<Flight>(flight)).Returns(expectedFlight);

            // Act
            adminController.AddFlight(flight);

            // Assert
            flightService.Verify(f => f.AddFlight(expectedFlight), Times.Once);
        }

        [Test]
        public void RenderIndexView_WhenEverythingIsDone()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();
            var adminController = new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object);

            var flight = new FlightModel();

            var expectedFlight = new Flight() { Id = 1 };
            mappingService.Setup(m => m.Map<Flight>(flight)).Returns(expectedFlight);
            flightService.Setup(f => f.AddFlight(expectedFlight)).Verifiable();

            // Act and Assert
            adminController.WithCallTo(a => a.AddFlight(flight))
                .ShouldRenderView("Index");
        }
    }
}