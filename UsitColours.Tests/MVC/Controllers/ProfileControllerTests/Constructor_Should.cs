using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Areas.Profile.Controllers;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMappingServiceMessage_WhenMappingServiceIsNull()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            Assert.That(() =>
          new ProfileController(null, mockedUserService.Object),
         Throws.InstanceOf<NullReferenceException>().With.Message.Contains("MappingService"));
        }

        [Test]
        public void ThrowNullReferenceWithUserServiceMessage_WhenUserServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();

            // Act and Assert
            Assert.That(() =>
          new ProfileController(mockedMappingService.Object, null),
         Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UserService"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new ProfileController(mockedMappingService.Object, mockedUserService.Object));
        }
    }
}

//public ProfileController(IMappingService mappingService, IUserService userService)
//{
//    if (mappingService == null)
//    {
//        throw new NullReferenceException("MappingService");
//    }

//    if (userService == null)
//    {
//        throw new NullReferenceException("UserService");
//    }

//    this.userService = userService;
//    this.mappingService = mappingService;
//}