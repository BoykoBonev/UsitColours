using NUnit.Framework;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Tests.Models
{
    [TestFixture]
    public class CountryTests
    {

        [Test]
        public void ConstructorShouldCreateCountry_WithoutParams()
        {
            // Act & Assert
            var country = new Country();

            Assert.IsInstanceOf<Country>(country);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new Country());
        }

        [Test]
        public void CountryShouldCreate_HashsetOfCities_WhenInitialized()
        {
            // Arrange
            var country = new Country();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<City>), country.Cities);
        }

        [Test]
        public void CountryShouldCreate_EmptyHashsetOfCities_WhenInitialized()
        {
            // Arrange
            var country = new Country();
            var expectedCollection = new HashSet<City>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, country.Cities);
        }

        [Test]
        public void CountryShouldSet_CorrectProperties_WhenValidArgsPassed()
        {
            // Arrange
            string name = "CountryName1";
            var expectedCities = new HashSet<City>();

            var country = new Country(name);

            // Act & Assert
            Assert.AreEqual(name, country.Name);
            CollectionAssert.AreEqual(expectedCities, country.Cities);
        }
    }
}
