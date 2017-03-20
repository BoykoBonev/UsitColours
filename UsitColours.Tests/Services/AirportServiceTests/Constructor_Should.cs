using Moq;
using NUnit.Framework;
using System;
using UsitColours.Data.Contracts;
using UsitColours.Services;

namespace UsitColours.Tests.Services.AirportServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceExceptionWithMessageContainingUsitData_WhenUsitDataIsNull()
        {
            // Arrange Act and Assert
            Assert.That(() =>
          new AirportService(null),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void NotThrow_WhenUsitDataIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();

            // Act and Assert
            Assert.DoesNotThrow(() => new AirportService(mockedData.Object));
        }
    }
}
