using System;
using System.Linq;
using System.Web.Mvc;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Controllers
{
    public class JobController : BaseController
    {
        private readonly IMappingService mappingService;
        private readonly IJobService jobService;
        private readonly IUserService userService;

        public JobController(IMappingService mappingService, IJobService jobService, IUserService userService)
        {
            if (mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            if (jobService == null)
            {
                throw new NullReferenceException("JobService");
            }

            if (userService == null)
            {
                throw new NullReferenceException("UserService");
            }

            this.mappingService = mappingService;
            this.jobService = jobService;
            this.userService = userService;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Home");
            }
            int jobId = (int)id;
            var job = this.jobService.GetJobById(jobId);

            var mappedJob = this.mappingService.Map<JobViewModel>(job);

            return View(mappedJob);
        }

        [Authorize]
        public ActionResult Buy(int jobId)
        {
            var userId = base.GetLoggedUserId;

            bool hasEnoughMoney = this.userService.AttachJobToUser(userId, jobId);

            if (hasEnoughMoney)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // TODO- Message for fail transaction
                return View("Index");
            }
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult SearchResult(string searchTerm, int page = 0)
        {

            var jobsSearchResult = this.jobService.GetAllJobsFromByTerm(searchTerm, page);

            var jobs = jobsSearchResult.Jobs;
            var count = jobsSearchResult.Count;

            var mappedJobs = jobs.Select(j => this.mappingService.Map<JobBaseViewModel>(j))
                .ToList();

            var searchViewModel = new SearchJobViewModel()
            {
                Jobs = mappedJobs,
                Count = count,
                SearchTerm = searchTerm
            };

            return PartialView("_SearchJobResult", searchViewModel);
        }





    }
}