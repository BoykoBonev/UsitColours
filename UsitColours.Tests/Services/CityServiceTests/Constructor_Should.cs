using Moq;
using NUnit.Framework;
using System;
using UsitColours.Data;
using UsitColours.Data.Contracts;
using UsitColours.Services;

namespace UsitColours.Tests.Services.CityServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceException_WhenUsitDataIsNull()
        {
            // Arrange, Act and Assert
            Assert.That(() =>
           new CityService( null),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void NotThrow_WhenUsitDataIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();

            // Act and Assert
            Assert.DoesNotThrow(() => new CityService(mockedData.Object));
        }
    }
}
