using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Profile.Controllers;
using UsitColours.Areas.Profile.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class FlightHistory_Should
    {
        [TestCase("User1")]
        [TestCase("User2")]

        public void CallGetFlightHistoryOnUserSeriveWithProvidedUserId(string userId)
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();
            var profileController = new ProfileController(mockedMappingService.Object, mockedUserService.Object);

            profileController.GetLoggedUserId = () => userId;

            // Act
            profileController.FlightHistory();

            // Assert
            mockedUserService.Verify(u => u.GetFlightHistory(userId), Times.Once);
        }

        [Test]
        public void CallMapOnMappingServiceOnProvidedFlightCollection()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();
            var profileController = new ProfileController(mockedMappingService.Object, mockedUserService.Object);

            var flights = new List<Flight>()
            {
                new Flight() {Id = 1 },
                new Flight() {Id = 2 },
            };

            mockedUserService.Setup(u => u.GetFlightHistory(It.IsAny<string>())).Returns(flights);

            profileController.GetLoggedUserId = () => It.IsAny<string>();


            // Act
            profileController.FlightHistory();

            // Assert

            foreach (var flight in flights)
            {
                mockedMappingService.Verify(m => m.Map<FlightStatisticViewModel>(flight), Times.Once);
            }
        }

        [Test]
        public void ReturnPartialView_FlightList_WithProvidedVIewModel()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();
            var profileController = new ProfileController(mockedMappingService.Object, mockedUserService.Object);

            var flights = new List<Flight>()
            {
                new Flight() {Id = 1 },
                new Flight() {Id = 2 },
            };

            profileController.GetLoggedUserId = () => It.IsAny<string>();

            mockedUserService.Setup(u => u.GetFlightHistory(It.IsAny<string>())).Returns(flights);

            var flightMapResult = new FlightStatisticViewModel()
            {
                Price = 1
            };

            mockedMappingService.Setup(m => m.Map<FlightStatisticViewModel>(It.IsAny<Flight>())).Returns(flightMapResult);

            // Act and Assert
            profileController
               .WithCallTo(h => h.FlightHistory())
               .ShouldRenderPartialView("_FlightList")
             .WithModel<IEnumerable<FlightStatisticViewModel>>(viewModel =>
             {
                 Assert.AreEqual(flights.Count, viewModel.Count());

                 foreach (var item in viewModel)
                 {
                     Assert.AreEqual(1, item.Price);
                 }
             });
        }
    }
}
