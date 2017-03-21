using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Data.Contracts;
using UsitColours.Services;

namespace UsitColours.Tests.Services.TicketServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceExceptionWithMessageContainingUsitData_whenusitDataIsNull()
        {
            // Arrange, Act and Assert
            Assert.That(() =>
              new TicketService(null),
              Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void NotThrow_WhenUsitDataIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();

            // Act and Assert
            Assert.DoesNotThrow(() => new TicketService(mockedData.Object));
        }
    }
}

