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
    public class AddFlight_Should
    {
        [Test]
        public void CallAddOnFlightRepositoryOnUsitDataOnce_WhenCityIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedMapedFlightFactory = new Mock<IMappedClassFactory>();

            var flightService = new FlightService(mockedMapedFlightFactory.Object, mockedData.Object);

            var mockedFlightRepository = new Mock<IGenericRepository<Flight>>();

            mockedData.Setup(d => d.Flights).Returns(mockedFlightRepository.Object);

            var flight = new Flight() { Id = 2 };

            // Act
            flightService.AddFlight(flight);

            // Assert
            mockedFlightRepository.Verify(r => r.Add(flight), Times.Once);
        }

        [Test]
        public void CallSaveChangesOnUsitData_WhenCityIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedMapedFlightFactory = new Mock<IMappedClassFactory>();

            var flightService = new FlightService(mockedMapedFlightFactory.Object, mockedData.Object);

            var mockedFlightRepository = new Mock<IGenericRepository<Flight>>();

            mockedData.Setup(d => d.Flights).Returns(mockedFlightRepository.Object);

            var flight = new Flight() { Id = 2 };

            // Act
            flightService.AddFlight(flight);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}