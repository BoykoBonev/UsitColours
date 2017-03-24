using Moq;
using NUnit.Framework;
using System;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;
using UsitColours.Tests.MVC.Mocks;

namespace UsitColours.Tests.MVC.Controllers.CommonControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMessageContainingCityService_WhenCityServiceIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();

            // Act and Assert
            Assert.That(() =>
                      new TestController(mappingService.Object, null, airportService.Object),
                     Throws.InstanceOf<NullReferenceException>().With.Message.Contains("CityService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingMappingService_WhenMappingServiceNull()
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();

            // Act and Assert
            Assert.That(() =>
                      new TestController(null, cityService.Object, airportService.Object),
                     Throws.InstanceOf<NullReferenceException>().With.Message.Contains("MappingService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingAirportService_WhenAirportServiceNull()
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var mappingService = new Mock<IMappingService>();

            // Act and Assert
            Assert.That(() =>
                      new TestController(mappingService.Object, cityService.Object,null),
                     Throws.InstanceOf<NullReferenceException>().With.Message.Contains("AirportService"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new TestController(mappingService.Object, cityService.Object, airportService.Object));
        }
    }
}
