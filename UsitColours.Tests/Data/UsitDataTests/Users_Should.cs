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
    public class Users_Should
    {
        [Test]
        public void ReturnInstanceOfGenericRepository()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();
            var usitData = new UsitData(dbContext.Object);

            // Act & Assert
            Assert.IsInstanceOf<IGenericRepository<ApplicationUser>>(usitData.Users);
        }

        [Test]
        public void ReturnSameInstance_WhenCalledMoreThanOnce()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();
            var usitData = new UsitData(dbContext.Object);

            // Act
            var expected = usitData.Users;
            var actual = usitData.Users;

            // Assert
            Assert.AreSame(expected, actual);
        }
    }
}
