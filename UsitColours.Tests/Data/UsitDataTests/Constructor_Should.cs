using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Data;
using UsitColours.Data.Contracts;

namespace UsitColours.Tests.Data.UsitDataTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNullReferenceWithMessageContainingDbContext_WhenDbContextIsNull()
        {
            // Arrange, Act and Assert
            Assert.That(() =>
        new UsitData(null),
        Throws.InstanceOf<NullReferenceException>().With.Message.Contains("DbContext"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();

            // Act and Assert
            Assert.DoesNotThrow(() => new UsitData(dbContext.Object));
        }
    }
}
