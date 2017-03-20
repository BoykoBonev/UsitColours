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

namespace UsitColours.Tests.Services.AirportServiceTests
{
    [TestFixture]
    public class AddAirport_Should
    {
        [Test]
        public void CallAddOnAirportsOnUsitDataOnce_WhenAirportIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var airportService = new AirportService(mockedData.Object);

            var mockedAirportRepository = new Mock<IGenericRepository<Airport>>();

            mockedData.Setup(d => d.Airports).Returns(mockedAirportRepository.Object);

            var airport = new Airport() { Id = 2, Name = "Name" };

            // Act
            airportService.AddAirport(airport);

            // Assert
            mockedAirportRepository.Verify(r => r.Add(airport), Times.Once);
        }

        [Test]
        public void CallSaveChangesOnUsitData_WhenAirportIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var airportService = new AirportService(mockedData.Object);

            var mockedAirportRepository = new Mock<IGenericRepository<Airport>>();

            mockedData.Setup(d => d.Airports).Returns(mockedAirportRepository.Object);

            var airport = new Airport() { Id = 2, Name = "Name" };

            // Act
            airportService.AddAirport(airport);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
