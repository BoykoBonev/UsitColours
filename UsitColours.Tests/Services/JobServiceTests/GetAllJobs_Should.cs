using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;

namespace UsitColours.Tests.Services.JobServiceTests
{
    [TestFixture]
    public class GetAllJobs_Should
    {
        [Test]
        public void CallAllOnJobsRepositoryOnUsitData_Once()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var jobService = new JobService(mockedData.Object);

            var mockedJobRepository = new Mock<IGenericRepository<Job>>();

            mockedData.Setup(d => d.Jobs).Returns(mockedJobRepository.Object);

            // Act
            jobService.GetAllJobs();

            // Assert
            mockedJobRepository.Verify(r => r.All, Times.Once);
        }
    }
}
