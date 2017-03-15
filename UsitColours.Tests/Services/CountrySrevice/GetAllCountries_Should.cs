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

namespace UsitColours.Tests.Services.CountrySrevice
{
    [TestFixture]
    public class GetAllCountries_Should
    {
        [Test]
        public void ReturnExpectedCollectionOfCountries()
        {
            // Arrange
            var usitData = new Mock<IUsitData>();
            var locationFactory = new Mock<ILocationFactory>();
            var countryRepositoryMock = new Mock<IGenericRepository<Country>>();

            var countryService = new CountryService(locationFactory.Object, usitData.Object);

            var expectedCollection = new List<Country> { new Country() { Id = 1, Name = "First" }, new Country() { Id = 2, Name = "I dont know" } }.AsQueryable();

            countryRepositoryMock.Setup(c => c.All).Returns(expectedCollection);
            usitData.Setup(x => x.Countries).Returns(countryRepositoryMock.Object);


            // Act
            var actualCollection = countryService.GetAllCountries();

            // Assert
            Assert.AreEqual(expectedCollection, actualCollection);
        }
    }
}
