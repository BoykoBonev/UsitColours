using Moq;
using NUnit.Framework;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;

namespace UsitColours.Tests.Services.CityServiceTests
{
    [TestFixture]
    public class AddCity_Should
    {
        [Test]
        public void CallAddOnCitiesOnUsitDataOnce_WhenCityIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var cityService = new CityService(mockedData.Object);

            var mockedCityRepository = new Mock<IGenericRepository<City>>();

            mockedData.Setup(d => d.Cities).Returns(mockedCityRepository.Object);

            var city = new City() { Id = 2, Name = "Name" };

            // Act
            cityService.AddCity(city);

            // Assert
            mockedCityRepository.Verify(r => r.Add(city), Times.Once);
        }

        [Test]
        public void CallSaveChangesOnUsitData_WhenCityIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var cityService = new CityService(mockedData.Object);

            var mockedCityRepository = new Mock<IGenericRepository<City>>();

            mockedData.Setup(d => d.Cities).Returns(mockedCityRepository.Object);

            var city = new City() { Id = 2, Name = "Name" };

            // Act
            cityService.AddCity(city);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
