using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Tests.MVC.Mocks;

namespace UsitColours.Tests.MVC.Controllers.CommonControllerTests
{
    [TestFixture]
    public class LoadAirports_Should
    {
        [TestCase(1)]
        [TestCase(21)]
        public void CallGetAllAirportsInCityOnAirportServiceWithExpectedId(int id)
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var testController = new TestController(mappingService.Object, cityService.Object, airportService.Object);

            // Act
            testController.LoadAirports(id);

            // Assert
            airportService.Verify(c => c.GetAllAirportsInCity(id), Times.Once);
        }

        [Test]
        public void ReturnExpectedJson()
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var testController = new TestController(mappingService.Object, cityService.Object, airportService.Object);

            int id = 1;
            string name = "Name";

            var airportViewModel = new AirportViewModel()
            {
                Id = id,
                 Name = name
            };

            mappingService.Setup(m => m.Map<AirportViewModel>(It.IsAny<Airport>())).Returns(airportViewModel);

            var airports = new List<Airport>()
            {
                new Airport(),
                new Airport(),
                new Airport(),
                new Airport(),
            };

            airportService.Setup(c => c.GetAllAirportsInCity(It.IsAny<int>())).Returns(airports);

            // Act ans Assert
            // Act & Assert
            var json = testController
                .WithCallTo(h => h.LoadAirports(It.IsAny<int>()))
                .ShouldReturnJson(data =>
                {
                    var resultData = (IEnumerable<SelectListItem>)data;

                    Assert.AreEqual(4, resultData.Count());

                    foreach (var item in resultData)
                    {
                        Assert.AreEqual(item.Value, id.ToString());
                        Assert.AreEqual(item.Text, name);
                    }
                });
        }
    }
}
