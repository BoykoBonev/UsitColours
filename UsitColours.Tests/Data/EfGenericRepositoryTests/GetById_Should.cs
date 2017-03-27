using Moq;
using NUnit.Framework;
using System.Data.Entity;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Tests.Data.Mocks;

namespace UsitColours.Tests.Data.EfGenericRepositoryTests
{
    [TestFixture]
    public class GetById_Should
    {
        [TestCase(2, 31)]
        [TestCase(12, 43)]
        public void CallFindOnDbSetWithExpectedId(int id, int secondId)
        {
            // Arrange
            var mockedDbContext = new Mock<IDbContext>();

            var mockedSet = new Mock<DbSet<Dummy>>();

            mockedDbContext.Setup(c => c.Set<Dummy>()).Returns(mockedSet.Object);

            var efRepository = new EfRepositoryMock<Dummy>(mockedDbContext.Object);

            // Act
            var actualItem = efRepository.GetById(id);

            // Assert
            mockedSet.Verify(s => s.Find(It.Is<int>(i => i == id)), Times.Once);
        }

        [Test]
        public void ReturnExpectedResult()
        {
            // Arrange
            var mockedDbContext = new Mock<IDbContext>();
            var mockedSet = new Mock<DbSet<Dummy>>();
            mockedDbContext.Setup(c => c.Set<Dummy>()).Returns(mockedSet.Object);
            var efRepository = new EfGenericRepository<Dummy>(mockedDbContext.Object);

            var expectedDummy = new Dummy() { Id = It.IsAny<int>(), Name = It.IsAny<string>() };

            mockedSet.Setup(s => s.Find(It.IsAny<int>())).Returns(expectedDummy);

            // Act
            var actualDummy = efRepository.GetById(It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedDummy, actualDummy);
        }
    }
}
