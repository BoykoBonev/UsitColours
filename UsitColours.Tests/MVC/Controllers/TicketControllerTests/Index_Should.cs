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
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.TicketControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultViewWithTempData()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mappingService = new Mock<IMappingService>();
            var ticketController = new TicketController(userService.Object, mappingService.Object);

            // Act and Assert
            ticketController.WithCallTo(t => t.Index())
                .ShouldRenderDefaultView();
        }
    }
}