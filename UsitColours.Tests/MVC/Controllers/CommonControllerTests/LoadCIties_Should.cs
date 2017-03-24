using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Tests.MVC.Mocks;
using System.Linq;

namespace UsitColours.Tests.MVC.Controllers.CommonControllerTests
{
    [TestFixture]
    public class LoadCIties_Should
    {
        [TestCase(1)]
        [TestCase(21)]
        public void CallGetCityInCountryOnCityServiceWithExpectedId(int id)
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var testController = new TestController(mappingService.Object, cityService.Object, airportService.Object);

            // Act
            testController.LoadCities(id);

            // Assert
            cityService.Verify(c => c.GetCityInCountry(id), Times.Once);
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

            var cityVIewModel = new CityViewModel()
            {
                Id = id,
                Name = name
            };

            mappingService.Setup(m => m.Map<CityViewModel>(It.IsAny<City>())).Returns(cityVIewModel);

            var cities = new List<City>()
            {
                new City(),
                new City(),
                new City(),
                new City(),
            };

            cityService.Setup(c => c.GetCityInCountry(It.IsAny<int>())).Returns(cities);

            // Act ans Assert
            // Act & Assert
            var json = testController
                .WithCallTo(h => h.LoadCities(It.IsAny<int>()))
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
