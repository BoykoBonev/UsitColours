using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.FlightControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMessageContainingFlightService_WhenFlightServiceIsNull()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();

            // Act and Assert
            Assert.That(() =>
            new FlightController(null, mappingService.Object, countryService.Object, airportService.Object, cityService.Object),
            Throws.InstanceOf<NullReferenceException>().With.Message.Contains("FlightService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingCountryService_WhenCountryServiceIsNull()
        {
            // Arrange
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();

            // Act and Assert
            Assert.That(() =>
            new FlightController(flightService.Object, mappingService.Object, null, airportService.Object, cityService.Object),
            Throws.InstanceOf<NullReferenceException>().With.Message.Contains("CountryService"));
        }

        [Test]
        public void NotThrowWhenEverythingIsPassed()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();

            // Act and Assert
            
            Assert.DoesNotThrow(() => new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object));
        }
    }
}
