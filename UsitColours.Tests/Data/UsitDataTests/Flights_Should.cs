using Moq;
using NUnit.Framework;
using UsitColours.Data;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;

namespace UsitColours.Tests.Data.UsitDataTests
{
    [TestFixture]
    class Flights_Should
    {
        [Test]
        public void ReturnInstanceOfGenericRepository()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();
            var usitData = new UsitData(dbContext.Object);

            // Act & Assert
            Assert.IsInstanceOf<IGenericRepository<Flight>>(usitData.Flights);
        }

        [Test]
        public void ReturnSameInstance_WhenCalledMoreThanOnce()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();
            var usitData = new UsitData(dbContext.Object);

            // Act
            var expected = usitData.Flights;
            var actual = usitData.Flights;

            // Assert
            Assert.AreSame(expected, actual);
        }
    }
}
