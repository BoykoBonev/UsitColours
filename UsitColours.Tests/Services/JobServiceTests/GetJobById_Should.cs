using Moq;
using NUnit.Framework;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;

namespace UsitColours.Tests.Services.JobServiceTests
{
    [TestFixture]
    public class GetJobById_Should
    {
        [TestCase(21)]
        [TestCase(1)]
        public void CallGetByIdOnJobsRepositoryOnUsitDataWithExpectedId_Once(int id)
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var jobService = new JobService(mockedData.Object);

            var mockedJobRepository = new Mock<IGenericRepository<Job>>();

            mockedData.Setup(d => d.Jobs).Returns(mockedJobRepository.Object);

            // Act
            jobService.GetJobById(id);

            // Assert
            mockedJobRepository.Verify(r => r.GetById(id), Times.Once);
        }
    }
}