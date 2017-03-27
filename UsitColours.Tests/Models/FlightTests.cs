using NUnit.Framework;
using System;
using System.Collections.Generic;
using UsitColours.Models;

namespace UsitColours.Tests.Models
{
    [TestFixture]
    public class FlightTest
    {
        [Test]
        public void ConstructorShouldCreateFlight_WithoutParams()
        {
            // Act & Assert
            var flight = new Flight();

            Assert.IsInstanceOf<Flight>(flight);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new Flight());
        }

        [Test]
        public void FlightShouldCreate_HashsetOfTickets_WhenInitialized()
        {
            // Arrange
            var flight = new Flight();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Ticket>), flight.Tickets);
        }

        [Test]
        public void FlightShouldCreate_EmptyHashsetOfTickets_WhenInitialized()
        {
            // Arrange
            var flight = new Flight();
            var expectedCollection = new HashSet<Ticket>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, flight.Tickets);
        }

        [Test]
        public void FlightShouldSet_CorrectProperties_WhenValidArgsPassed()
        {

            // Arrange
            int airlineId = 1;
            int airportArrivalId = 1;
            int airportDepartureId = 2;
            DateTime arrivalDate = new DateTime(2020, 01, 01, 10, 10, 50);
            DateTime departureDate = new DateTime(2020, 01, 01, 09, 10, 50);
            decimal price = 10m;
            int sits = 10;
            var expectedTickets = new HashSet<Ticket>();

            var flight = new Flight(airlineId, airportArrivalId, airportDepartureId, arrivalDate, departureDate, price, sits);

            // Act & Assert
            Assert.AreEqual(airlineId, flight.AirlineId);
            Assert.AreEqual(airportArrivalId, flight.AirportArrivalId);
            Assert.AreEqual(airportDepartureId, flight.AirportDepartureId);
            Assert.AreEqual(arrivalDate, flight.DateOfArrival);
            Assert.AreEqual(departureDate, flight.DateOfDeparture);
            Assert.AreEqual(price, flight.Price);
            Assert.AreEqual(sits, flight.AvailableSeats);
            CollectionAssert.AreEqual(expectedTickets, flight.Tickets);
        }
    }
}
