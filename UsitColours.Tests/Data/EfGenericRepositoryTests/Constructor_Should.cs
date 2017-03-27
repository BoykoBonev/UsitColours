using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Tests.Data.Mocks;

namespace UsitColours.Tests.Data.EfGenericRepositoryTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_NullReference_WithMessageContaining_DbContext()
        {
            Assert.That(
              () => new EfGenericRepository<Dummy>(null),
               Throws.InstanceOf<NullReferenceException>().With.Message.Contains("DbContext"));
        }

        [Test]
        public void NotThrow_WhenEverythingIsSet()
        {
            // Arrange
            var mockedDbContext = new Mock<IDbContext>();

            // Act and Assert
            Assert.DoesNotThrow(() => new EfGenericRepository<Dummy>(mockedDbContext.Object));
        }

        [Test]
        public void SetDbSet_WithCorrectSet()
        {
            // Arrange
            var mockedDbContext = new Mock<IDbContext>();

            var dbSetMock = QueryableDbSetMock.GetQueryableMockDbSet<Dummy>(new List<Dummy>() { new Dummy() });

            mockedDbContext.Setup(c => c.Set<Dummy>()).Returns(dbSetMock);

            // Act
            var efRepositoryMock = new EfRepositoryMock<Dummy>(mockedDbContext.Object);

            // Assert
            Assert.AreEqual(dbSetMock, efRepositoryMock.MyDbSet);
        }
    }
}
