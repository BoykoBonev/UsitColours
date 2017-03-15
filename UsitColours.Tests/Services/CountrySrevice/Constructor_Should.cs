using Moq;
using NUnit.Framework;
using System;
using UsitColours.Data.Contracts;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.CountrySrevice
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceExceptionWithMessageContaining_UsitData_WhenUsitDataIsNull()
        {
            // Arrange
            var locationFactory = new Mock<ILocationFactory>();

            // Act and Assert
            Assert.That(() => 
            new CountryService(locationFactory.Object, null),
            Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void ThrowNullReferenceExceptionWithMessageContaining_LocationFactory_WhenLocationFactoryIsNull()
        {
            // Arrange
            var usitData = new Mock<IUsitData>();

            // Act and Assert
            Assert.That(() =>
            new CountryService(null, usitData.Object),
            Throws.InstanceOf<NullReferenceException>().With.Message.Contains("LocationFactory"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var usitData = new Mock<IUsitData>();
            var locationFactory = new Mock<ILocationFactory>();

            // Act and Assert
            Assert.DoesNotThrow(() => new CountryService(locationFactory.Object, usitData.Object));
        }
    }
}