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
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.JobControllerTests
{
    [TestFixture]
    public class Buy_Should
    {
        [TestCase("first", 2)]
        [TestCase("second", 3)]

        public void CallAttachJobToUserWithProvidedParameters(string userId, int jobId)
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            jobController.GetLoggedUserId = () => userId;

            // Act
            jobController.Buy(jobId);

            // Assert
            userService.Verify(u => u.AttachJobToUser(userId, jobId), Times.Once);
        }

        [Test]
        public void RedirectToHomeConntrollerIndexActionWhenAttachJobReturnTrue()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            jobController.GetLoggedUserId = () => It.IsAny<string>();

            userService.Setup(u => u.AttachJobToUser(It.IsAny<string>(), It.IsAny<int>())).Returns(true);

            // Act and Assert
            jobController
                .WithCallTo(t => t.Buy(It.IsAny<int>()))
                .ShouldRedirectTo<HomeController>(typeof(HomeController).GetMethod("Index"));
        }

        [Test]
        public void ReturnIndexViewWhenAttachJobToUserReturnFalse()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            jobController.GetLoggedUserId = () => It.IsAny<string>();

            userService.Setup(u => u.AttachJobToUser(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

            // Act and Assert
            jobController
                .WithCallTo(t => t.Buy(It.IsAny<int>()))
                .ShouldRenderView("Index");
        }

        [Test]
        public void HaveAuthorizeAttribute()
        {
            // Arrange
            var method = typeof(JobController).GetMethod(nameof(JobController.Buy));

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }
    }
}
