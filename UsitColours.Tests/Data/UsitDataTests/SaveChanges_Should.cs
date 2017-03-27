using Moq;
using NUnit.Framework;
using UsitColours.Data;
using UsitColours.Data.Contracts;

namespace UsitColours.Tests.Data.UsitDataTests
{
    [TestFixture]
    public class SaveChanges_Should
    {
        [Test]
        public void CallSaveChangesOnDbContext_Once()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();
            var usitData = new UsitData(dbContext.Object);

            // Act
            usitData.SaveChanges();

            // Assert
            dbContext.Verify(d => d.SaveChanges(), Times.Once());
        }
    }
}