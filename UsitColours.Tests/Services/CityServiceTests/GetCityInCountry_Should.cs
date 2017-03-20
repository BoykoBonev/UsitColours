using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;

namespace UsitColours.Tests.Services.CityServiceTests
{
    [TestFixture]
    public class GetCityInCountry_Should
    {
        [TestCase(20)]
        [TestCase(2)]
        public void WhenCountryIdIsPassed_ShoudReturnExpectedCollection(int countryId)
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var cityService = new CityService(mockedData.Object);

            var mockedCityRepository = new Mock<IGenericRepository<City>>();

            var cityCollection = new List<City>()
            {
                new City() {Id = It.IsAny<int>(), CountryId = countryId, Name = "First city" },
                 new City() {Id = It.IsAny<int>(), CountryId = 33, Name = "Second city" },
                  new City() {Id = It.IsAny<int>(), CountryId = countryId, Name = "Third city" },
                   new City() {Id = It.IsAny<int>(), CountryId = 45, Name = "Fourth city" }
            }.AsQueryable();

            mockedData.Setup(d => d.Cities).Returns(mockedCityRepository.Object);
            mockedCityRepository.Setup(r => r.All).Returns(cityCollection);

            var exepectedCities = cityCollection
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.Name)
                .ToList();

            // Act
            var actualCities = cityService.GetCityInCountry(countryId);

            // Assert
            CollectionAssert.AreEqual(exepectedCities, actualCities);
        }
    }
}
