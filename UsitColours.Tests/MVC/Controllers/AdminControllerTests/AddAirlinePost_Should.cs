using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Controllers;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.AdminControllerTests
{
    [TestFixture]
    public class AddAirlinePost_Should
    {
        [TestCase("Name1")]
        [TestCase("Name2")]
        public void CallAddAirlineOnAirlineServiceWithExpectedName(string name)
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();
            var adminController = new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object);

            // Act 
            adminController.AddAirline(name);

            // Assert
            airlineService.Verify(a => a.AddAirline(name), Times.Once);
        }

        public void RenderIndexView(string name)
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();
            var adminController = new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object);

            // Act 
            adminController.WithCallTo(a => a.AddAirline(It.IsAny<string>()))
                .ShouldRenderView("Index");
        }
    }
}
