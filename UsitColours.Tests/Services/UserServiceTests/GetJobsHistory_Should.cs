using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class GetJobsHistory_Should
    {
        [Test]
        public void ReturnExpectedCollectionOfUserWithPassedId_WhenPerformedExpectedExpressions()
        {
            // Arrange
            var userRepositoryStub = new Mock<IGenericRepository<ApplicationUser>>();
            var mockedUsitData = new Mock<IUsitData>();
            var ticketFactoryMock = new Mock<ITicketFactory>();

            var userService = new UserService(mockedUsitData.Object, ticketFactoryMock.Object);

            mockedUsitData.Setup(d => d.Users).Returns(userRepositoryStub.Object);


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


            var expectedCollection = userCollectionAsQuaryable.Where(u => u.Id == userId).SelectMany(p => p.Jobs).OrderBy(t => t.StartDate);

            // Act
            var actualCollection = userService.GetJobsHistory(userId);

            // Assert
            CollectionAssert.AreEqual(expectedCollection.ToList(), actualCollection.ToList());
        }
    }
}
