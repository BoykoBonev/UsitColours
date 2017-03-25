using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.JobControllerTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void RenderDefaultView()
        {
            // Arrange
            var mappingService = new Mock<IMappingService>();
            var jobService = new Mock<IJobService>();
            var userService = new Mock<IUserService>();
            var jobController = new JobController(mappingService.Object, jobService.Object, userService.Object);

            // Act and Assert
            jobController.WithCallTo(j => j.Search())
                .ShouldRenderDefaultView();
        }
    }
}
