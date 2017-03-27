using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Data;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;

namespace UsitColours.Tests.Data.UsitDataTests
{
    [TestFixture]
    class Airlines_Should
    {
        [Test]
        public void ReturnInstanceOfGenericRepository()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();
            var usitData = new UsitData(dbContext.Object);

            // Act & Assert
            Assert.IsInstanceOf<IGenericRepository<Airline>>(usitData.Airlines);
        }

        [Test]
        public void ReturnSameInstance_WhenCalledMoreThanOnce()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();
            var usitData = new UsitData(dbContext.Object);

            // Act
            var expected = usitData.Airlines;
            var actual = usitData.Airlines;

            // Assert
            Assert.AreSame(expected, actual);
        }
    }
}
