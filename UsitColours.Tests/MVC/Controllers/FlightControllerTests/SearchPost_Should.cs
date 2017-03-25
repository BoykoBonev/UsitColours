using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Models;

namespace UsitColours.Tests.MVC.Controllers.FlightControllerTests
{
    [TestFixture]
    public class SearchPost_Should
    {
        [TestCase(1, 2, 3)]
        [TestCase(12, 32, 3)]
        [TestCase(11, 12, 33)]
        public void CallGetFlightsWithExpectedParameters(int airportDepartureId, int airportArrivalId, int availableSeats)
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var date = new DateTime(2017, 10, 10);

            // Act
            flightController.Search(airportDepartureId, airportArrivalId, date, availableSeats);

            // Assert
            flightService.Verify(f => f.GetFlights(airportDepartureId, airportArrivalId, date, availableSeats), Times.Once);
        }

        [Test]
        public void CallMappingServiceOnPassedFlights()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var flights = new List<PresentationFlight>()
            {
                new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()),
                 new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()),
                  new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>())
            };

            flightService.Setup(f => f.GetFlights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(flights);

            // Act
            flightController.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>());

            // Assert
            foreach (var flight in flights)
            {
                mappingService.Verify(m => m.Map<DetailsFlightViewModel>(flight), Times.Once);
            }
        }

        [Test]
        public void Render_FlightSearchResult_WithExpectedViewModel()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var flights = new List<PresentationFlight>()
            {
                new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()),
                 new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()),
                  new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>())
            };

            flightService.Setup(f => f.GetFlights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(flights);

            var detailsFlightViewModel = new DetailsFlightViewModel()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<DetailsFlightViewModel>(It.IsAny<PresentationFlight>())).Returns(detailsFlightViewModel);

            // Act ans Assert
            flightController.WithCallTo(f => f.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>()))
               .ShouldRenderPartialView("_FlightSearchResult")
               .WithModel<IEnumerable<DetailsFlightViewModel>>(m =>
               {
                   Assert.AreEqual(flights.Count, m.Count());
                   foreach (var item in m)
                   {
                       Assert.AreEqual(1, item.Id);
                   }
               });
        }

        [Test]
        public void HaveTemDataWithKeyTicketAndValueExpectedFlightViewModel()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var flights = new List<PresentationFlight>()
            {
                new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()),
                 new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()),
                  new PresentationFlight(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>())
            };

            flightService.Setup(f => f.GetFlights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(flights);

            var detailsFlightViewModel = new DetailsFlightViewModel()
            {
                Id = 1
            };

            mappingService.Setup(m => m.Map<DetailsFlightViewModel>(It.IsAny<PresentationFlight>())).Returns(detailsFlightViewModel);

            // Act 
            flightController.Search(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>());

            // Assert
            flightController.ShouldHaveTempDataProperty< IEnumerable<DetailsFlightViewModel>>("Ticket", m=> m.Count() == 3 );
        }
    }
}