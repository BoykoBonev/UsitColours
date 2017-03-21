using Moq;
using NUnit.Framework;
using System;
using UsitColours.Data.Contracts;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.FlightServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceExpcetionWithMessageContaining_UsitData_WhenUsitDataIsNull()
        {
            // Arrange
            var mockedMapedFlightFactory = new Mock<IMappedClassFactory>();

            // Act and Assert
            Assert.That(() =>
             new FlightService(mockedMapedFlightFactory.Object, null),
             Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void ThrowNullReferenceExpcetionWithMessageContaining_MappedClassFactory_WhenMappedClassFactoryNull()
        {
            // Arrange
            var mockedUsitData = new Mock<IUsitData>();

            // Act and Assert
            Assert.That(() =>
            new FlightService( null, mockedUsitData.Object),
            Throws.InstanceOf<NullReferenceException>().With.Message.Contains("MappedFactory"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var mockedUsitData = new Mock<IUsitData>();
            var mockedMapedFlightFactory = new Mock<IMappedClassFactory>();

            // Act and Assert
            Assert.DoesNotThrow(() => new FlightService(mockedMapedFlightFactory.Object, mockedUsitData.Object));
        }
    }
}