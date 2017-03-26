using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Controllers;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.AdminControllerTests
{
    [TestFixture]
    public class AddJobGet_Should
    {
        [Test]
        public void CallGetAllCountriesOnCountryServiceOnce()
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
            adminController.AddJob();

            // Assert
            countryService.Verify(c => c.GetAllCountries(), Times.Once);
        }

        [Test]
        public void Render_AddFlightPartialViewWithExpectedViewModel()
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

            var countries = new List<Country>()
            {
                new Country() {Id = 1, Name = "First"},
                new Country() {Id = 2, Name = "Second" },
                new Country() {Id = 3, Name = "Third" }
            };

            countryService.Setup(c => c.GetAllCountries()).Returns(countries);

            adminController.WithCallTo(f => f.AddJob())
               .ShouldRenderPartialView("_AddJob")
               .WithModel<AddJobViewModel>(m =>
               {
                   var countriesList = m.Countries.ToList();
                   Assert.AreEqual(countries.Count, countriesList.Count);
                   Assert.AreEqual(countries[0].Id.ToString(), countriesList[0].Value);
                   Assert.AreEqual(countries[0].Name, countriesList[0].Text);
                   Assert.AreEqual(countries[1].Id.ToString(), countriesList[1].Value);
                   Assert.AreEqual(countries[1].Name, countriesList[1].Text);
                   Assert.AreEqual(countries[2].Id.ToString(), countriesList[2].Value);
                   Assert.AreEqual(countries[2].Name, countriesList[2].Text);
               });
        }
    }
}
