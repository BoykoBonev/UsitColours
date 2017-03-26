using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsitColours.Areas.Profile.Models;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Services.Contracts;

namespace UsitColours.Areas.Profile.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IMappingService mappingService;
        private readonly IUserService userService;

        public ProfileController(IMappingService mappingService, IUserService userService)
        {
            if (mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            if (userService == null)
            {
                throw new NullReferenceException("UserService");
            }

            this.userService = userService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlightHistory()
        {
            var userId = this.GetLoggedUserId();

            var flights = this.userService.GetFlightHistory(userId)
                .Select(f => mappingService.Map<FlightStatisticViewModel>(f))
                .ToList();

            return PartialView("_FlightList", flights);
        }

        public ActionResult UpcommingFlights()
        {
            var userId = this.GetLoggedUserId();
            var flights = this.userService.GetUpcommingFlights(userId)
                 .Select(f => mappingService.Map<FlightStatisticViewModel>(f))
                .ToList();


            return PartialView("_FlightList", flights);
        }

        public ActionResult JobHistory()
        {
            var userId = this.GetLoggedUserId();
            var jobs = this.userService.GetJobsHistory(userId)
                 .Select(f => mappingService.Map<JobStatisticViewModel>(f))
                .ToList();

            return PartialView("_JobList", jobs);

        }

        public ActionResult UpcommingJobs()
        {
            var userId = this.GetLoggedUserId();
            var jobs = this.userService.GetUpcommingJobs(userId)
                .Select(f => mappingService.Map<JobStatisticViewModel>(f))
                .ToList();

            return PartialView("_JobList", jobs);
        }

    }
}