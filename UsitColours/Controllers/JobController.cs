using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Controllers
{
    public class JobController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IJobService jobService;

        public JobController(IMappingService mappingService, IJobService jobService)
        {
            if(mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            if (jobService == null)
            {
                throw new NullReferenceException("JobService");
            }
            this.mappingService = mappingService;
            this.jobService = jobService;
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

    }
}