using NUnit.Framework;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Tests.Models
{
    [TestFixture]
    public class AirporTest
    {
        [Test]
        public void ConstructorShouldCreateAirport_WithoutParams()
        {
            // Act & Assert
            var airport = new Airport();

            Assert.IsInstanceOf<Airport>(airport);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new Airport());
        }

        [Test]
        public void AirportShouldCreate_HashsetOfArrivalFlights_WhenInitialized()
        {
            // Arrange
            var airport = new Airport();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Flight>), airport.Arrival);
        }

        [Test]
        public void AirportShouldCreate_HashsetOfDepartureFlights_WhenInitialized()
        {
            // Arrange
            var airport = new Airport();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Flight>), airport.Departure);
        }

        [Test]
        public void AirportShouldCreate_EmptyHashsetOfArrivalFlights_WhenInitialized()
        {
            // Arrange
            var airport = new Airport();
            var expectedCollection = new HashSet<Flight>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, airport.Arrival);
        }

        [Test]
        public void AirportShouldCreate_EmptyHashsetOfDepartureFlightss_WhenInitialized()
        {
            // Arrange
            var airport = new Airport();
            var expectedCollection = new HashSet<Flight>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, airport.Departure);
        }

        [Test]
        public void AirportShouldSet_CorrectProperties_WhenValidArgsPassed()
        {
            // Arrange
            string name = "Airport1";
            int cityId = 1;
            var expectedArrivalFlights = new HashSet<Flight>();
            var expectedDepartureFlights = new HashSet<Flight>();


            var airport = new Airport(name, cityId);

            // Act & Assert
            Assert.AreEqual(name, airport.Name);
            Assert.AreEqual(cityId, airport.CityId);
            CollectionAssert.AreEqual(expectedArrivalFlights, airport.Arrival);
            CollectionAssert.AreEqual(expectedDepartureFlights, airport.Departure);
        }
    }
}