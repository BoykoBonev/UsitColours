using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestStack.FluentMVCTesting;
using UsitColours.Areas.Admin.Controllers;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Controllers.AdminControllerTests
{
    [TestFixture]
    public class AddJobPost_Should
    {
        [Test]
        public void RedirectToIndexAction()
        {
            // Arrange
            var countryService = new Mock<ICountryService>();
            var airlineService = new Mock<IAirlineService>();
            var flightService = new Mock<IFlightService>();
            var mappingService = new Mock<IMappingService>();
            var cityService = new Mock<ICityService>();
            var airportService = new Mock<IAirportService>();
            var jobService = new Mock<IJobService>();
            var adminController = new AdminController(countryService.Object, mappingService.Object, cityService.Object, airlineService.Object, airportService.Object, flightService.Object, jobService.Object);

            var httpPostedFile = new Mock<HttpPostedFileBase>();
            adminController.ModelState.AddModelError(string.Empty, new Exception());

            var job = new JobViewModel() { Id = 1};

            // Act and Assert
            adminController.WithCallTo(a => a.AddJob(job, httpPostedFile.Object))
               .ShouldRedirectTo(typeof(AdminController).GetMethod("Index"));
        }


    }
}

//public ActionResult AddJob(JobViewModel job, HttpPostedFileBase file)
//{
//    if (!this.ModelState.IsValid)
//    {
//        return this.View(job);
//    }

//    string path = string.Empty;
//    if (job.IsDefaultImage)
//    {
//        path = System.IO.Path.Combine(
//                         Server.MapPath("~/images"), "job-default.jpg");
//    }
//    else
//    {
//        string pic = System.IO.Path.GetFileName(file.FileName);
//        path = System.IO.Path.Combine(
//                         Server.MapPath("~/images"), pic);

//        file.SaveAs(path);
//    }

//    job.ImagePath = path;


//    var jobModel = base.MappingService.Map<Job>(job);

//    this.jobService.AddJob(jobModel);

//    return View("Index");