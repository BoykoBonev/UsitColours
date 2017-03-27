using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Profile.Controllers;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();
            var profileController = new ProfileController(mockedMappingService.Object, mockedUserService.Object);

            // Act and Assert
            profileController
              .WithCallTo(h => h.Index())
              .ShouldRenderDefaultView();
        }
    }
}
