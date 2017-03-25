using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.FlightControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        public void ReturnHomeVIew_WhenIdIsNull()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            // Act and Assert
            flightController
               .WithCallTo(f => f.Details(null))
               .ShouldRenderView("Home");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void CallGetDetailedFlightOnFlightServiceWithExpectedId(int? id)
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            // Act
            flightController.Details(id);
            int flightId = (int)id;

            // Assert
            flightService.Verify(f => f.GetDetailedFlight(flightId), Times.Once);
        }

        [Test]
        public void CllMapOnMappingServiceWithExpectedFlightObject()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var flight = new Flight()
            {
                Id = 1
            };

            flightService.Setup(f => f.GetDetailedFlight(It.IsAny<int>())).Returns(flight);

            // Act
            // shoud not be null
            flightController.Details(10);

            // Assert
            mappingService.Verify(m => m.Map<DetailsFlightViewModel>(flight), Times.Once);
        }

        [Test]
        public void RenderDetailsFlightPartialViewWithExpectedMappedFlight()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var cityService = new Mock<ICityService>();
            var flightController = new FlightController(flightService.Object, mappingService.Object, countryService.Object, airportService.Object, cityService.Object);

            var flight = new Flight()
            {
                Id = It.IsAny<int>()
            };

            flightService.Setup(f => f.GetDetailedFlight(It.IsAny<int>())).Returns(flight);

            var expectedFlight = new DetailsFlightViewModel()
            {
                Id = It.IsAny<int>()
            };
            mappingService.Setup(m => m.Map<DetailsFlightViewModel>(flight)).Returns(expectedFlight);

            // Act and Assert
            flightController.WithCallTo(f => f.Details(10))
                .ShouldRenderPartialView("_DetailFlight")
                .WithModel<DetailsFlightViewModel>(m =>
                {
                    Assert.AreEqual(expectedFlight, m);
                });
        }
    }
}
