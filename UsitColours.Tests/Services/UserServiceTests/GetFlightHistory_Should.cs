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
using UsitColours.Services.Utils;

namespace UsitColours.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class GetFlightHistory_Should
    {
        [Test]
        public void ReturnSameCollection_WhenExpectedExpressionsAreExecutedOnIt()
        {
            // Arrange
            var userRepositoryStub = new Mock<IGenericRepository<ApplicationUser>>();
            var mockedUsitData = new Mock<IUsitData>();
            var ticketFactory = new Mock<ITicketFactory>();

            var userService = new UserService(mockedUsitData.Object, ticketFactory.Object);

            var firstDateOfDeparture = new DateTime(2016, 10, 10, 10, 5, 5, 3);
            var secondDateOfDeparture = new DateTime(2016, 10, 10, 11, 5, 5, 3);
            var thirdDateOfDeparture = new DateTime(2016, 10, 10, 12, 5, 5, 3);
            string userId = "GuidGuid";

            var mockedDate = new DateTime(2016, 10, 10, 10, 5, 5, 3);
            var timeProvider = new Mock<TimeProvider>();

            timeProvider.Setup(x => x.GetDate()).Returns(mockedDate);
            TimeProvider.Current = timeProvider.Object;


            var ticketCollection = new List<Ticket>()
            {
                new Ticket()
                {
                    Flight = new Flight()
                    {
                        Id = It.IsAny<int>(),
                        DateOfDeparture = secondDateOfDeparture
                    },
                    Id = It.IsAny<int>()
                },
                new Ticket()
                {
                    Flight = new Flight()
                    {
                        Id = It.IsAny<int>(),
                        DateOfDeparture = thirdDateOfDeparture
                    },
                    Id = It.IsAny<int>()
                },
                 new Ticket()
                {
                    Flight = new Flight()
                    {
                        Id = It.IsAny<int>(),
                        DateOfDeparture = firstDateOfDeparture
                    },
                    Id = It.IsAny<int>()
                }
            };

            var user = new ApplicationUser()
            {
                Id = userId,
                Tickets = ticketCollection
            };

            var userCollection = new List<ApplicationUser>();
            userCollection.Add(user);

            var userCollectionAsQuaryable = userCollection.AsQueryable();

            mockedUsitData.Setup(d => d.Users).Returns(userRepositoryStub.Object);

            userRepositoryStub.Setup(u => u.All).Returns(userCollectionAsQuaryable);


            var expectedCollection = userCollectionAsQuaryable
                .Where(u => u.Id == userId)
                .SelectMany(p => p.Tickets)
                .Select(t => t.Flight)
                .Where(f => f.DateOfDeparture < mockedDate)
                .OrderBy(t => t.DateOfDeparture)
                .ToList();

            // Act
            var actualCollection = userService.GetFlightHistory(userId);

            // Assert
            CollectionAssert.AreEqual(expectedCollection.ToList(), actualCollection.ToList());
        }
    }
}