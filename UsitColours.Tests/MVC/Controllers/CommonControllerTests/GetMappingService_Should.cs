using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;
using UsitColours.Tests.MVC.Mocks;

namespace UsitColours.Tests.MVC.Controllers.CommonControllerTests
{
    [TestFixture]
    public class GetMappingService_Should
    {
        [Test]
        public void ReturnExpectedObject()
        {
            // Arrange
            var cityService = new Mock<ICityService>();
            var mappingService = new Mock<IMappingService>();
            var airportService = new Mock<IAirportService>();
            var testController = new TestController(mappingService.Object, cityService.Object, airportService.Object);

            // Act
            var actualMappingService = testController.GetMappingService;

            // Assert
            Assert.AreSame(mappingService.Object, actualMappingService);
        }
    }
}

