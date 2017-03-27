using NUnit.Framework;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Tests.Models
{
    [TestFixture]
    public class AirlineTest
    {
        [Test]
        public void ConstructorShouldCreateAirline_WithoutParams()
        {
            // Act & Assert
            var airline = new Airline();

            Assert.IsInstanceOf<Airline>(airline);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new Airline());
        }

        [Test]
        public void AirlineShouldCreate_HashsetOfCities_WhenInitialized()
        {
            // Arrange
            var airline = new Airline();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<City>), airline.Cities);
        }

        [Test]
        public void AirlineShouldCreate_HashsetOfFlights_WhenInitialized()
        {
            // Arrange
            var airline = new Airline();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Flight>), airline.Flights);
        }

        [Test]
        public void AirlineShouldCreate_EmptyHashsetOfCities_WhenInitialized()
        {
            // Arrange
            var airline = new Airline();
            var expectedCollection = new HashSet<City>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, airline.Cities);
        }

        [Test]
        public void AirlineShouldCreate_EmptyHashsetOfFlights_WhenInitialized()
        {
            // Arrange
            var airline = new Airline();
            var expectedCollection = new HashSet<Flight>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, airline.Flights);
        }

        [Test]
        public void AirlineShouldSet_CorrectProperties_WhenValidArgsPassed()
        {
            // Arrange
            string name = "Airline1";
            var expectedCities = new HashSet<City>();
            var expectedFlights = new HashSet<Flight>();


            var airline = new Airline(name);

            // Act & Assert
            Assert.AreEqual(name, airline.Name);
            CollectionAssert.AreEqual(expectedCities, airline.Cities);
            CollectionAssert.AreEqual(expectedFlights, airline.Flights);
        }
    }
}
