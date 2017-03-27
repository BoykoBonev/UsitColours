using NUnit.Framework;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Tests.Models
{
    [TestFixture]
    public class CityTest
    {
        [Test]
        public void ConstructorShouldCreateCity_WithoutParams()
        {
            // Act & Assert
            var city = new City();

            Assert.IsInstanceOf<City>(city);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new City());
        }

        [Test]
        public void CityShouldCreate_HashsetOfAirports_WhenInitialized()
        {
            // Arrange
            var city = new City();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Airport>), city.Airports);
        }

        [Test]
        public void CityShouldCreate_HashsetOfJobs_WhenInitialized()
        {
            // Arrange
            var city = new City();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Job>), city.Jobs);
        }

        [Test]
        public void CityShouldCreate_EmptyHashsetOfAirports_WhenInitialized()
        {
            // Arrange
            var city = new City();
            var expectedCollection = new HashSet<Airport>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, city.Airports);
        }

        [Test]
        public void CityShouldCreate_EmptyHashsetOfJobs_WhenInitialized()
        {
            // Arrange
            var city = new City();
            var expectedCollection = new HashSet<Job>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, city.Jobs);
        }

        [Test]
        public void CityShouldSet_CorrectProperties_WhenValidArgsPassed()
        {
            // Arrange
            string name = "CityName1";
            int countryId = 1;
            var expectedAirports = new HashSet<Airport>();
            var expectedJobs = new HashSet<Job>();


            var city = new City(name, countryId);

            // Act & Assert
            Assert.AreEqual(name, city.Name);
            Assert.AreEqual(countryId, city.CountryId);
            CollectionAssert.AreEqual(expectedAirports, city.Airports);
            CollectionAssert.AreEqual(expectedJobs, city.Jobs);
        }
    }
}
