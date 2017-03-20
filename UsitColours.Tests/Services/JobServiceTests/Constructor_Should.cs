using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Data.Contracts;
using UsitColours.Services;

namespace UsitColours.Tests.Services.JobServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceExceptionWithMessageContainingUsitData_WhenUsitDataIsNull()
        {
            // Arrange, Act and Assert
            Assert.That(() =>
           new JobService(null),
           Throws.InstanceOf<NullReferenceException>().With.Message.Contains("UsitData"));
        }

        [Test]
        public void NotThrow_WhenUsitDataIsPassed()
        {
            // Arrange
            var mockedUsitData = new Mock<IUsitData>();

            // Act and Assert
            Assert.DoesNotThrow(() => new JobService(mockedUsitData.Object));
        }
    }
}


