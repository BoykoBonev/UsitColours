using Moq;
using NUnit.Framework;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.FlightServiceTests
{
    [TestFixture]
    public class GetDetailedFlight_Should
    {
        [TestCase(21)]
        [TestCase(1)]
        [TestCase(333)]

        public void CallGetByIdOnFlightRepositoryOfUsitDataWithPassedId(int id)
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedMapedFlightFactory = new Mock<IMappedClassFactory>();

            var flightService = new FlightService(mockedMapedFlightFactory.Object, mockedData.Object);

            var flightRepository = new Mock<IGenericRepository<Flight>>();

            mockedData.Setup(d => d.Flights).Returns(flightRepository.Object);

            // Act
            flightService.GetDetailedFlight(id);

            // Assert
            flightRepository.Verify(r => r.GetById(id), Times.Once);
        }
    }
}
