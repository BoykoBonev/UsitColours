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
    public class AddAirportGet_Should
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
            adminController.AddAirport();

            // Assert
            countryService.Verify(c => c.GetAllCountries(), Times.Once);
        }

        [Test]
        public void CallMapOnMappingServiceWithExpectedCountry()
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

            var countryViewModel = new CountryViewModel() { Id = 1, Name = "Name" };

            mappingService.Setup(m => m.Map<CountryViewModel>(It.IsAny<Country>())).Returns(countryViewModel);

            // Act
            adminController.AddAirport();

            // Assert
            foreach (var country in countries)
            {
                mappingService.Verify(m => m.Map<CountryViewModel>(country), Times.Once);
            }
        }

        [Test]
        public void RenderPartialView_AddAirport_WithExpectedViewModel()
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

            var countryViewModel = new CountryViewModel() { Id = 1, Name = "Name" };
            mappingService.Setup(m => m.Map<CountryViewModel>(It.IsAny<Country>())).Returns(countryViewModel);

            adminController.WithCallTo(f => f.AddAirport())
               .ShouldRenderPartialView("_AddAirport")
               .WithModel<AddAirportViewModel>(m =>
               {
                   Assert.AreEqual(countries.Count, m.Countries.Count);

                   foreach (var country in m.Countries)
                   {
                       Assert.AreEqual(country.Text, "Name");
                       Assert.AreEqual(country.Value, "1");
                   }
               });
        }
    }
}

