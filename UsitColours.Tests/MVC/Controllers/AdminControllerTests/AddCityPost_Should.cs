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
    public class AddCityPost_Should
    {
        [Test]
        public void ReturnDefaultViewWithPassedCityViewModel_WhenModelStateIsInvalid()
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

            adminController.ModelState.AddModelError(String.Empty, new Exception());

            var cityViewModel = new CityViewModel()
            {
                Id = 1
            };

            // Act and Assert
            adminController.WithCallTo(a => a.AddCity(cityViewModel))
                .ShouldRedirectTo(typeof(AdminController).GetMethod("Index"));
        }

        [Test]
        public void CallMapOnMappingServiceWithExpectedCityViewModel()
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

            var cityViewModel = new CityViewModel()
            {
                Id = 1
            };

            // Act
            adminController.AddCity(cityViewModel);

            // Assert
            mappingService.Verify(m => m.Map<City>(cityViewModel), Times.Once);
        }

        [Test]
        public void CallAddCityOnCityServiceWIthExpectedCity()
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

            var cityViewModel = new CityViewModel()
            {
                Id = 1
            };

            var city = new City()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<City>(cityViewModel)).Returns(city);

            // Act
            adminController.AddCity(cityViewModel);

            // Assert
            cityService.Verify(c => c.AddCity(city), Times.Once);
        }

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

            var cityViewModel = new CityViewModel()
            {
                Id = 1
            };

            var city = new City()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<City>(cityViewModel)).Returns(city);

            cityService.Setup(c => c.AddCity(It.IsAny<City>())).Verifiable();

            // Act and Assert
            adminController.WithCallTo(a => a.AddCity(cityViewModel))
               .ShouldRenderView("Index");
        }
    }
}