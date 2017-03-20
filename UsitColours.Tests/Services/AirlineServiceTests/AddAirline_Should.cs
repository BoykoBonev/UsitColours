using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.AirlineServiceTests
{
    [TestFixture]
    public class AddAirline_Should
    {
        [TestCase("First name")]
        [TestCase("Second name")]
        public void CallCreateAirlineOnAirlineFactoryWithPassedName_Once(string airlineName)
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedAirlineFactory = new Mock<IAirlineFactory>();
            var airlineService = new AirlineService(mockedData.Object, mockedAirlineFactory.Object);

            var mockedAirlineRepository = new Mock<IGenericRepository<Airline>>();

            mockedData.Setup(d => d.Airlines).Returns(mockedAirlineRepository.Object);

            // Act
            airlineService.AddAirline(airlineName);

            // Assert
            mockedAirlineFactory.Verify(f => f.CreateAirline(airlineName), Times.Once);
        }

        [TestCase("First name")]
        [TestCase("Second name")]
        public void CallAddOnAirlineRepositoryWithExpectedAirline_Once(string airlineName)
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedAirlineFactory = new Mock<IAirlineFactory>();
            var airlineService = new AirlineService(mockedData.Object, mockedAirlineFactory.Object);

            var mockedAirlineRepository = new Mock<IGenericRepository<Airline>>();

            mockedData.Setup(d => d.Airlines).Returns(mockedAirlineRepository.Object);

            var airline = new Airline() { Id = It.IsAny<int>(), Name = It.IsAny<string>() };
            mockedAirlineFactory.Setup(f => f.CreateAirline(airlineName)).Returns(airline);

            // Act
            airlineService.AddAirline(airlineName);

            // Assert
            mockedAirlineRepository.Verify(r => r.Add(airline), Times.Once);
        }

        [TestCase("First name")]
        [TestCase("Second name")]
        public void CallSaveChangesOnUsitData_Once(string airlineName)
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedAirlineFactory = new Mock<IAirlineFactory>();
            var airlineService = new AirlineService(mockedData.Object, mockedAirlineFactory.Object);

            var mockedAirlineRepository = new Mock<IGenericRepository<Airline>>();

            mockedData.Setup(d => d.Airlines).Returns(mockedAirlineRepository.Object);

            var airline = new Airline() { Id = It.IsAny<int>(), Name = It.IsAny<string>() };
            mockedAirlineFactory.Setup(f => f.CreateAirline(airlineName)).Returns(airline);

            // Act
            airlineService.AddAirline(airlineName);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
