using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Controllers;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.AdminControllerTests
{
    [TestFixture]
    public class AddCountryPost_Should
    {
        [TestCase("Name1")]
        [TestCase("Name2")]
        [TestCase("Name3")]

        public void CallCountryServiceWithExpectedName(string name)
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
            adminController.AddCountry(name);

            // Assert
            countryService.Verify(c => c.AddCountry(name), Times.Once);
        }
        
        [Test]
        public void RenterIndexViewWhenNameIsPassed()
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

            // Act and Assert
            adminController.WithCallTo(a => a.AddCountry(It.IsAny<string>()))
                .ShouldRenderView("Index");
        }
    }
}