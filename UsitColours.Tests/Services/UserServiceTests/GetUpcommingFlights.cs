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
    public class GetUpcommingFlights
    {
        [Test]
        public void ReturnExpectedCollectionOfFlights_WhenExecutedSameExpressions()
        {
            // Arrange
            var userRepositoryStub = new Mock<IGenericRepository<ApplicationUser>>();
            var ticketFactoryMock = new Mock<ITicketFactory>();

            var mockedUsitData = new Mock<IUsitData>();
            var userService = new UserService(mockedUsitData.Object, ticketFactoryMock.Object) ;

            mockedUsitData.Setup(d => d.Users).Returns(userRepositoryStub.Object);

            var mockedDate = new DateTime(2016, 10, 10, 10, 5, 5, 3);
            var timeProvider = new Mock<TimeProvider>();

            timeProvider.Setup(x => x.GetDate()).Returns(mockedDate);
            TimeProvider.Current = timeProvider.Object;

            var firstDateOfDeparture = new DateTime(2016, 10, 10, 10, 5, 5, 1);
            var secondDateOfDeparture = new DateTime(2016, 10, 10, 11, 5, 5, 3);
            var thirdDateOfDeparture = new DateTime(2016, 10, 10, 12, 5, 5, 3);
            string userId = "GuidGuid";

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

            userRepositoryStub.Setup(u => u.All).Returns(userCollectionAsQuaryable);


            var expectedCollection = userCollectionAsQuaryable.Where(u => u.Id == userId).SelectMany(p => p.Tickets).Select(t => t.Flight).Where(f => f.DateOfDeparture > mockedDate).OrderBy(t => t.DateOfDeparture);

            // Act
            var actualCollection = userService.GetUpcommingFlights(userId);

            // Assert
            CollectionAssert.AreEqual(expectedCollection.ToList(), actualCollection.ToList());

            TimeProvider.ResetToDefault();
        }
    }
}
