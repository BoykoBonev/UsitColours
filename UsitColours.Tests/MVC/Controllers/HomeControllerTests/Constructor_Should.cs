using Moq;
using NUnit.Framework;
using System;
using UsitColours.AutoMapper;
using UsitColours.Common;
using UsitColours.Controllers;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMessageContainingFlightService_WhenFlightServiceIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var cacheProvider = new Mock<ICacheProvider>();

            // Act and Assert
            Assert.That(() =>
            new HomeController(null, mappingService.Object, jobService.Object, cacheProvider.Object),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("FlightService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingMappingService_WhenMappingServiceIsNull()
        {
            // Arrange
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();
            var cacheProvider = new Mock<ICacheProvider>();

            // Act and Assert
            Assert.That(() =>
            new HomeController(flightService.Object, null, jobService.Object, cacheProvider.Object),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("MappingService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContaininJobService_WhenJobServiceIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var flightService = new Mock<IFlightService>();
            var cacheProvider = new Mock<ICacheProvider>();

            // Act and Assert
            Assert.That(() =>
            new HomeController(flightService.Object, mappingService.Object, null, cacheProvider.Object),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("JobService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContaininCacheProvider_WhenCacheProviderIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();

            // Act and Assert
            Assert.That(() =>
            new HomeController(flightService.Object, mappingService.Object, jobService.Object, null),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("CacheProvider"));
        }
        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var flightService = new Mock<IFlightService>();
            var jobService = new Mock<IJobService>();
            var cacheProvider = new Mock<ICacheProvider>();

            // Act and Assert
            Assert.DoesNotThrow(() => new HomeController(flightService.Object, mappingService.Object, jobService.Object, cacheProvider.Object));
        }
    }
}
