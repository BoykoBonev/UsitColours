using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Data.Contracts;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceExceptionWithMessageContaining_UsitData_WhenUsitDataIsNull()
        {
            // Arrange
            var ticketFactoryMock = new Mock<ITicketFactory>();

            // Act and Assert
            Assert.That(() =>
            new UserService(null, ticketFactoryMock.Object),
             Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void ThrowNullReferenceExceptionWithMessageContaining_TicketFactory_WhenUsitDataIsNull()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();

            // Act and Assert
            Assert.That(() =>
            new UserService(mockedData.Object, null),
             Throws.InstanceOf<NullReferenceException>().With.Message.Contains("TicketFactory"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var ticketFactoryMock = new Mock<ITicketFactory>();

            // Act and Assert
            Assert.DoesNotThrow(() => new UserService(mockedData.Object, ticketFactoryMock.Object));
        }
    }
}