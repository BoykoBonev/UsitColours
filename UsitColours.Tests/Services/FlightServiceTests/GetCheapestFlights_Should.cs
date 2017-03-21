using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;
using UsitColours.Services.Utils;

namespace UsitColours.Tests.Services.FlightServiceTests
{
    [TestFixture]
    public class GetCheapestFlights_Should
    {
        [Test]
        public void ReturnExpectedCollection_WhenExpectedExpressionsArePerformedOnCollection()
        {
            // Arrange
            var flightRepositoryStub = new Mock<IGenericRepository<Flight>>();
            var mockedData = new Mock<IUsitData>();
            var mappedFactory = new Mock<IMappedClassFactory>();
            var flightService = new FlightService(mappedFactory.Object, mockedData.Object);

            mockedData.Setup(d => d.Flights).Returns(flightRepositoryStub.Object);

            var mockedDate = new DateTime(2016, 10, 10, 10, 5, 5, 3);
            var timeProvider = new Mock<TimeProvider>();

            timeProvider.Setup(x => x.GetDate()).Returns(mockedDate);
            TimeProvider.Current = timeProvider.Object;

            var dateAfter = new DateTime(2016, 10, 11, 10, 5, 5, 3);
            var secondDateAfter = new DateTime(2016, 10, 12, 10, 5, 5, 3);
            var dateBefore = new DateTime(2016, 5, 12, 10, 5, 5, 3);


            var flights = new List<Flight>()
            {
                new Flight()
                {
                    DateOfDeparture = secondDateAfter,
                    Price = 20,
                    AvailableSeats = 0
                },
                 new Flight()
                {
                    DateOfDeparture = dateAfter,
                    Price = 1,
                    AvailableSeats = 1

                },
                  new Flight()
                {
                    DateOfDeparture = dateBefore,
                    Price = 200,
                    AvailableSeats  = 3

                },
                    new Flight()
                {
                    DateOfDeparture = dateAfter,
                    Price = 123,
                    AvailableSeats = 33

                },
                      new Flight()
                {
                    DateOfDeparture = dateAfter,
                    Price = 14,
                    AvailableSeats = 0

                },
                        new Flight()
                {
                    DateOfDeparture = dateAfter,
                    Price = 11,
                    AvailableSeats = 10
                },

            }.AsQueryable();

            flightRepositoryStub.Setup(f => f.All).Returns(flights);

            var take = 3;

            var expectedFlights = flights.Where(f => f.DateOfDeparture > mockedDate && f.AvailableSeats > 0).Include(f => f.AirportArrival).Include(f => f.AirportDeparture).OrderBy(f => f.Price).Take(take);

            // Act
            var actualFlights = flightService.GetCheapestFlights();

            // Assert
            CollectionAssert.AreEqual(expectedFlights, actualFlights);
        }
    }
}