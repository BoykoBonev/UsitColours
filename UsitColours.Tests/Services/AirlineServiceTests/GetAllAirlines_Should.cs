using Moq;
using NUnit.Framework;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;
using UsitColours.Models;
using UsitColours.Services;
using UsitColours.Services.Contracts.Factories;

namespace UsitColours.Tests.Services.AirlineServiceTests
{
    [TestFixture]
    public class GetAllAirlines_Should
    {
        [Test]
        public void CallAllOnAirlineRepositoryOnUsitData_WhenMethodIsCalled()
        {
            // Arrange
            var mockedData = new Mock<IUsitData>();
            var mockedAirlineFactory = new Mock<IAirlineFactory>();
            var airlineService = new AirlineService(mockedData.Object, mockedAirlineFactory.Object);

            var mockedAirlineRepository = new Mock<IGenericRepository<Airline>>();

            mockedData.Setup(d => d.Airlines).Returns(mockedAirlineRepository.Object);

            // Act
            airlineService.GetAllAirlines();

            // Assert
            mockedAirlineRepository.Verify(r => r.All, Times.Once);
        }
    }
}