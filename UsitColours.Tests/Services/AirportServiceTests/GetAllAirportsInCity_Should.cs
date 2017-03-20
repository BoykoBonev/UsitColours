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
    public class GetAllAirportsInCity_Should
    {
        [TestCase(22)]
        [TestCase(1)]
        public void ReturnExpectedAirportCollectionWhenCityIdIsPassed(int cityId)
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var airportService = new AirportService(mockedData.Object);

            var mockedAiportRepository = new Mock<IGenericRepository<Airport>>();

            mockedData.Setup(d => d.Airports).Returns(mockedAiportRepository.Object);

            var airports = new List<Airport>()
            {
                new Airport() {Id = It.IsAny<int>(), Name = "Second" },
                new Airport() {Id = cityId, Name = "First" },
                new Airport() {Id = cityId, Name = "Third" },
                new Airport() {Id = It.IsAny<int>(), Name = "Fourth" },
            }.AsQueryable();

            mockedAiportRepository.Setup(r => r.All).Returns(airports);

            var expectedAirports = airports
                 .Where(a => a.CityId == cityId)
                 .OrderBy(a => a.Name)
                 .ToList();

            // Act
            var actualAirports = airportService.GetAllAirportsInCity(cityId);

            // Assert
            CollectionAssert.AreEqual(expectedAirports, actualAirports);
        }
    }
}
