using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Models;

namespace UsitColours.Tests.MVC.Controllers.TicketControllerTests
{
    [TestFixture]
    public class Buy_Should
    {
        [Test]
        public void CallMappingServiceOnExpectedFlightCollection()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mappingService = new Mock<IMappingService>();
            var ticketController = new TicketController(userService.Object, mappingService.Object);

            var userId = "userId";
            ticketController.GetLoggedUserId = () => userId;

            var flights = new List<DetailsFlightViewModel>()
            {
                new DetailsFlightViewModel() {Id = 1 },
                new DetailsFlightViewModel() {Id = 2 },
                new DetailsFlightViewModel() {Id = 3 },
            };

            ticketController.TempData["Ticket"] = flights;
            
            // Act
            ticketController.Buy();

            // Assert
            foreach (var flight in flights)
            {
                mappingService.Verify(m => m.Map<PresentationFlight>(flight), Times.Once);
            }
        }

        [TestCase("first")]
        [TestCase("second")]
        [TestCase("third")]
        public void CallBuyTicketOnUserServiceWithEpectedParameters(string currentUserId)
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mappingService = new Mock<IMappingService>();
            var ticketController = new TicketController(userService.Object, mappingService.Object);

            var userId = "userId";
            ticketController.GetLoggedUserId = () => userId;

            var flights = new List<DetailsFlightViewModel>()
            {
                new DetailsFlightViewModel() {Id = 1 },
                new DetailsFlightViewModel() {Id = 2 },
                new DetailsFlightViewModel() {Id = 3 },
            };

            ticketController.TempData["Ticket"] = flights;

            var mappingPresentationFlightResult = new PresentationFlight(It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>());
           
            mappingService.Setup(m => m.Map<PresentationFlight>(It.IsAny<DetailsFlightViewModel>())).Returns(mappingPresentationFlightResult);

            // Act
            ticketController.Buy();

            // Assert
            userService.Verify(u => u.BuyTicket(userId, It.Is<IEnumerable<PresentationFlight>>(p => p.Count() == flights.Count)), Times.Once);
        }

        [Test]
        public void RedirectToHomeControllerIndex_WhenBuyTicketReturnTrue()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mappingService = new Mock<IMappingService>();
            var ticketController = new TicketController(userService.Object, mappingService.Object);

            var userId = "userId";
            ticketController.GetLoggedUserId = () => userId;

            var flights = new List<DetailsFlightViewModel>()
            {
                new DetailsFlightViewModel() {Id = 1 },
                new DetailsFlightViewModel() {Id = 2 },
                new DetailsFlightViewModel() {Id = 3 },
            };

            ticketController.TempData["Ticket"] = flights;

            var mappingPresentationFlightResult = new PresentationFlight(It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>());

            mappingService.Setup(m => m.Map<PresentationFlight>(It.IsAny<DetailsFlightViewModel>())).Returns(mappingPresentationFlightResult);

            userService.Setup(u => u.BuyTicket(It.IsAny<string>(), It.IsAny<IEnumerable<PresentationFlight>>())).Returns(true);

            // Act and Assert
            ticketController
                .WithCallTo(t => t.Buy())
                .ShouldRedirectTo<HomeController>(typeof(HomeController).GetMethod("Index"));
        }

        [Test]
        public void RenderIndexView_WhenBuyTicketIsFalse()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mappingService = new Mock<IMappingService>();
            var ticketController = new TicketController(userService.Object, mappingService.Object);

            var userId = "userId";
            ticketController.GetLoggedUserId = () => userId;

            var flights = new List<DetailsFlightViewModel>()
            {
                new DetailsFlightViewModel() {Id = 1 },
                new DetailsFlightViewModel() {Id = 2 },
                new DetailsFlightViewModel() {Id = 3 },
            };

            ticketController.TempData["Ticket"] = flights;

            var mappingPresentationFlightResult = new PresentationFlight(It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>());

            mappingService.Setup(m => m.Map<PresentationFlight>(It.IsAny<DetailsFlightViewModel>())).Returns(mappingPresentationFlightResult);

            userService.Setup(u => u.BuyTicket(It.IsAny<string>(), It.IsAny<IEnumerable<PresentationFlight>>())).Returns(false);

            // Act and Assert
            ticketController
                .WithCallTo(t => t.Buy())
                .ShouldRenderView("Index");
        }

        [Test]
        public void HaveAuthorizeAttribute()
        {
            // Arrange
            var method = typeof(TicketController).GetMethod(nameof(TicketController.Buy));

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }
    }
}