using Moq;
using NUnit.Framework;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;
using UsitColours.Tests.MVC.Mocks;

namespace UsitColours.Tests.MVC.Controllers.CommonControllerTests
{
    [TestFixture]
    public class GetAirportService_Should
    {
        [Test]
        public void ReturnExpectedObject()
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var testController = new TestController(mappingService.Object, cityService.Object, airportService.Object);

            // Act
            var actualAirportService = testController.GetAirportService;

            // Assert
            Assert.AreSame(airportService.Object, actualAirportService);
        }
    }
}
