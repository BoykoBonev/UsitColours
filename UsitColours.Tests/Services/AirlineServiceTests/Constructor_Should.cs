using Moq;
using NUnit.Framework;
using System;
using UsitColours.Data.Contracts;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.AirlineServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceExceptionWithUsitDataInMessage_WhenUsitDataIsNull()
        {
            // Arrange
            var mockedAirlineFactory = new Mock<IAirlineFactory>();

            // Act and Assert
            Assert.That(() =>
          new AirlineService(null, mockedAirlineFactory.Object),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void ThrowNullReferenceExceptionWithAirlineFactoryInMessage_WhenUsitDataIsNull()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();

            // Act and Assert
            Assert.That(() =>
          new AirlineService(mockedData.Object, null),
          Throws.InstanceOf<NullReferenceException>().With.Message.Contains("AirlineFactory"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedAirlineFactory = new Mock<IAirlineFactory>();

            // Act and Assert

            Assert.DoesNotThrow(() => new AirlineService(mockedData.Object, mockedAirlineFactory.Object));
        }
    }
}