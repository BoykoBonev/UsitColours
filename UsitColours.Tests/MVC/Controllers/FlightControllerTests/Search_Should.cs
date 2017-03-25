using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.FlightControllerTests
{
    [TestFixture]
    class Search_Should
    {
        [Test]
        public void CallGetAllCountriesOnCountryServiceOnce()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            // Act
            flightController.Search();

            // Assert
            countryService.Verify(c => c.GetAllCountries(), Times.Once);
        }

        [Test]
        public void CallMappingServiceAsManyTimesAsCountOfTheCountries()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var countries = new List<Country>()
            {
                new Country() {Id =1, Name = It.IsAny<string>()},
                new Country() {Id = 2, Name = It.IsAny<string>()},
                new Country() {Id = 3,Name = It.IsAny<string>() },
                new Country() {Id = 4,Name = It.IsAny<string>() }
            };

            countryService.Setup(c => c.GetAllCountries()).Returns(countries);

            var countryViewModel = new CountryViewModel() { Id = 1, Name = "First" };

            mappingService.Setup(m => m.Map<CountryViewModel>(It.IsAny<Country>())).Returns(countryViewModel);
            // Act
            flightController.Search();

            // Assert
            foreach (var country in countries)
            {
                mappingService.Verify(m => m.Map<CountryViewModel>(country), Times.Once);
            }
        }

        [Test]
        public void RenderDefaultViewWithExpectedViewModel()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var countries = new List<Country>()
            {
                new Country() {Id =1, Name = It.IsAny<string>()},
                new Country() {Id = 2, Name = It.IsAny<string>()},
                new Country() {Id = 3,Name = It.IsAny<string>() },
                new Country() {Id = 4,Name = It.IsAny<string>() }
            };

            countryService.Setup(c => c.GetAllCountries()).Returns(countries);

            var countryViewModel = new CountryViewModel() { Id = 1, Name = "First" };

            mappingService.Setup(m => m.Map<CountryViewModel>(It.IsAny<Country>())).Returns(countryViewModel);

            // Act and Assert
            flightController.WithCallTo(f => f.Search())
                .ShouldRenderDefaultView()
                .WithModel<SearchViewModel>(m =>
                {
                    foreach (var country in m.Countries)
                    {
                        Assert.AreEqual("First", country.Text);
                        Assert.AreEqual("1", country.Value);
                    }
                });
        }
    }
}