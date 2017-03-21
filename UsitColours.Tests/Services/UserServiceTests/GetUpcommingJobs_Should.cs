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
using UsitColours.Services.Contracts.Factories;
using UsitColours.Services.Utils;

namespace UsitColours.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class GetUpcommingJobs_Should
    {
        [Test]
        public void ReturnExpectedCollectionOfUserWithPassedId_WhenPerformedExpectedExpressions()
        {
            // Arrange
            var userRepositoryStub = new Mock<IGenericRepository<ApplicationUser>>();
            var ticketFactoryMock = new Mock<ITicketFactory>();
            var mockedUsitData = new Mock<IUsitData>();

            var userService = new UserService(mockedUsitData.Object, ticketFactoryMock.Object);

            mockedUsitData.Setup(d => d.Users).Returns(userRepositoryStub.Object);
            var mockedDate = new DateTime(2016, 10, 10, 10, 5, 5, 3);
            var timeProvider = new Mock<TimeProvider>();

            timeProvider.Setup(x => x.GetDate()).Returns(mockedDate);
            TimeProvider.Current = timeProvider.Object;

            var firstDateOfDeparture = new DateTime(2016, 10, 10, 10, 5, 5, 1);
            var secondDateOfDeparture = new DateTime(2016, 10, 10, 11, 5, 5, 3);
            var thirdDateOfDeparture = new DateTime(2016, 10, 10, 12, 5, 5, 3);
            string userId = "GuidGuid";

            var jobs = new List<Job>()
            {
                new Job()
                {

                        StartDate = secondDateOfDeparture,
                    Id = It.IsAny<int>()
                },
                new Job()
                {

                        StartDate = thirdDateOfDeparture,
                    Id = It.IsAny<int>()
                },
                 new Job()
                {

                    StartDate = firstDateOfDeparture,
                    Id = It.IsAny<int>()
                }
            };

            var user = new ApplicationUser()
            {
                Id = userId,
                Jobs = jobs
            };

            var userCollection = new List<ApplicationUser>();
            userCollection.Add(user);

            var userCollectionAsQuaryable = userCollection.AsQueryable();

            userRepositoryStub.Setup(u => u.All).Returns(userCollectionAsQuaryable);


            var expectedCollection = userCollectionAsQuaryable.Where(u => u.Id == userId).SelectMany(p => p.Jobs).Where(f => f.StartDate > mockedDate).OrderBy(t => t.StartDate);

            // Act
            var actualCollection = userService.GetUpcommingJobs(userId);

            // Assert
            CollectionAssert.AreEqual(expectedCollection.ToList(), actualCollection.ToList());

            TimeProvider.ResetToDefault();
        }
    }
}

