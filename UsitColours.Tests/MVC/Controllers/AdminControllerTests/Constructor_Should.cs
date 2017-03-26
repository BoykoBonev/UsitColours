using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Areas.Admin.Controllers;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.AdminControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMessageContainingCountryService_WhenCountryServiceIsNull()
        {
            // Arrange
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();

            // Act and Assert
            Assert.That(() =>
          new AdminController(null, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("CountryService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingAirlineService_WhenAirlineServiceIsNull()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();

            // Act and Assert
            Assert.That(() =>
          new AdminController(countryService.Object, mappingService.Object, cityService.Object, null, airportService.Object, flightService.Object, jobService.Object),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("AirlineService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingFlightService_WhenFlightServiceIsNull()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var jobService = new Mock<IJobService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();

            // Act and Assert
            Assert.That(() =>
          new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, null, jobService.Object),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("FlightService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingJobService_WhenJobServiceIsNull()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();

            // Act and Assert
            Assert.That(() =>
          new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, null),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("JobService"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
          new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object));
        }
    }
}

