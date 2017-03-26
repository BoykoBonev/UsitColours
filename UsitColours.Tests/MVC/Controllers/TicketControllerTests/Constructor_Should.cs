using Moq;
using NUnit.Framework;
using System;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.TicketControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMessageContainingUserService_WhenUserServiceIsNull()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();

            // Act and Assert
            Assert.That(() =>
           new TicketController(null, mappingService.Object),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UserService"));
        }

        [Test]
        public void ThrowNullReferenceWithMessageContainingMappingService_WhenMappingServiceIsNull()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            // Act and Assert
            Assert.That(() =>
           new TicketController(userService.Object, null),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("MappingService"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mappingService = new Mock<IMappingService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new TicketController(userService.Object, mappingService.Object));
        }
    }
}