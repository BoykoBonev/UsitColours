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
    public class AddFlightGet_Should
    {
        [Test]
        public void CallGetAllCountries_OnCountryService()
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

            // Act
            adminController.AddFlight();

            // Assert
            countryService.Verify(c => c.GetAllCountries(), Times.Once);
        }

        [Test]
        public void CallGetAllAirlinesOnAirlineService()
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

            // Act
            adminController.AddFlight();

            // Assert
            airlineService.Verify(a => a.GetAllAirlines(), Times.Once);
        }

        [Test]
        public void Render_AddFlightPartialViewWithExpectedViewModel()
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

            var countries = new List<Country>()
            {
                new Country() {Id = 1, Name = "First"},
                new Country() {Id = 2, Name = "Second" },
                new Country() {Id = 3, Name = "Third" }
            };

            countryService.Setup(c => c.GetAllCountries()).Returns(countries);

            var airlines = new List<Airline>()
            {
                new Airline() {Id = 1, Name = "First"},
                new Airline() {Id = 2, Name = "Second" },
                new Airline() {Id = 3, Name = "Third" }
            };

            airlineService.Setup(c => c.GetAllAirlines()).Returns(airlines);

            adminController.WithCallTo(f => f.AddFlight())
               .ShouldRenderPartialView("_AddFlight")
               .WithModel<AddFlightViewModel>(m =>
               {
                   Assert.AreEqual(countries.Count, m.Countries.Count);
                   Assert.AreEqual(airlines.Count, m.Airlines.Count);
               });
        }
    }
}
