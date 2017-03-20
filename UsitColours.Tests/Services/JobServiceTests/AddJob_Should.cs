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
    public class AddJob_Should
    {
        [Test]
        public void CallAddOnAirportsOnUsitDataOnce_WhenAirportIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var jobService = new JobService(mockedData.Object);

            var mockedJobRepository = new Mock<IGenericRepository<Job>>();

            mockedData.Setup(d => d.Jobs).Returns(mockedJobRepository.Object);

            var job = new Job() { Id = 2 };

            // Act
            jobService.AddJob(job);

            // Assert
            mockedJobRepository.Verify(r => r.Add(job), Times.Once);
        }

        [Test]
        public void CallSaveChangesOnUsitData_WhenAirportIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var jobService = new JobService(mockedData.Object);

            var mockedJobRepository = new Mock<IGenericRepository<Job>>();

            mockedData.Setup(d => d.Jobs).Returns(mockedJobRepository.Object);

            var job = new Job() { Id = 2 };

            // Act
            jobService.AddJob(job);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
