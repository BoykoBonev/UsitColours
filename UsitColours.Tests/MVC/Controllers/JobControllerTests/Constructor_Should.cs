using Moq;
using NUnit.Framework;
using System;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.JobControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMessageContainingMappingService_WhenMappingServiceIsNull()
        {
            // Arrange
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();

            // Act and Assert
            Assert.That(() =>
            new JobController(null, jobService.Object, userService.Object),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("MappingService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingJobService_WhenJobServiceIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var userService = new Mock<IUserService>();

            // Act and Assert
            Assert.That(() =>
            new JobController(mappingService.Object, null, userService.Object),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("JobService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingUserService_WhenUserServiceIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();

            // Act and Assert
            Assert.That(() =>
            new JobController(mappingService.Object, jobService.Object, null),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UserService"));
        }

        [Test]
        public void NotThrowWhenEverythingIsPassed()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new JobController(mappingService.Object, jobService.Object, userService.Object));
        }

        public void InheritsBaseController()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();

            // Act
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            // Assert
            Assert.IsInstanceOf<BaseController>(jobController);
        }
    }
}
