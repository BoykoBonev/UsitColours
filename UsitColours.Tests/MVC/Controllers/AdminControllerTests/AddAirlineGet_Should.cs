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
    public class AddAirlineGet_Should
    {
        [Test]
        public void Return_AddAirlinePartialView()
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

            // Act and Assert
            adminController.WithCallTo(a => a.AddAirline())
                .ShouldRenderPartialView("_AddAirline");
        }
    }
}