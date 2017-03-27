using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Controllers;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.AdminControllerTests
{
    [TestFixture]
    public class AddAirportPosh_Should
    {
        [Test]
        public void RedirectToIndexActionWhenModelStateIsInvalid()
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

            var expectedAirportVIewModel = new AirportViewModel()
            {
                Id = 1
            };

            // Act and Assert
            adminController.WithCallTo(a => a.AddAirport(expectedAirportVIewModel))
                .ShouldRedirectTo(typeof(AdminController).GetMethod("Index"));
        }

        [Test]
        public void CallMapOnMappingServiceWithExpectedAirportViewModel()
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

            var expectedAirportVIewModel = new AirportViewModel()
            {
                Id = 1
            };

            // Act
            adminController.AddAirport(expectedAirportVIewModel);

            // Assert
            mappingService.Verify(m => m.Map<Airport>(expectedAirportVIewModel), Times.Once);
        }

        [Test]
        public void CallAddAirportOnAirportServiceWIthExpectedAirport()
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

            var expectedAirportVIewModel = new AirportViewModel()
            {
                Id = 1
            };

            var airport = new Airport()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<Airport>(expectedAirportVIewModel)).Returns(airport);

            // Act
            adminController.AddAirport(expectedAirportVIewModel);

            // Assert
            airportService.Verify(c => c.AddAirport(airport), Times.Once);
        }

        [Test]
        public void ReturnIndexView_WhenEverythingIsDone()
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

            var expectedAirportVIewModel = new AirportViewModel()
            {
                Id = 1
            };

            var airport = new Airport()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<Airport>(expectedAirportVIewModel)).Returns(airport);

            // Act and Assert
            adminController.WithCallTo(a => a.AddAirport(expectedAirportVIewModel))
                .ShouldRenderView("Index");
        }
    }
}
