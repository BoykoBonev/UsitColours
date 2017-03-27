using System;
using System.Linq;
using System.Web.Mvc;
using UsitColours.AutoMapper;
using UsitColours.Common;
using UsitColours.Constants;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Utils;

namespace UsitColours.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightService flightService;
        private readonly IMappingService mappingService;
        private readonly IJobService jobService;
        private readonly ICacheProvider cacheProvider;

        public HomeController(IFlightService flightService, IMappingService mappingService, IJobService jobService, ICacheProvider cacheProvider)
        {
            if (flightService == null)
            {
                throw new NullReferenceException("FlightService");
            }

            if (mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            if (jobService == null)
            {
                throw new NullReferenceException("JobService");
            }

            if (cacheProvider == null)
            {
                throw new NullReferenceException("CacheProvider");
            }

            this.cacheProvider = cacheProvider;
            this.jobService = jobService;
            this.mappingService = mappingService;
            this.flightService = flightService;
        }

        public ActionResult Index()
        {
            HomeViewModel homeViewModel = null;
            homeViewModel = (HomeViewModel)this.cacheProvider.GetValue(GlobalConstants.HomeCache);

            if (homeViewModel == null)
            {

                var cheapestFlights = this.flightService.GetCheapestFlights()
                    .Select(f => mappingService.Map<FlightVIewModel>(f))
                    .ToList();

                var soonestJobs = this.jobService.GetSoonestJobs()
                    .Select(j => mappingService.Map<JobViewModel>(j))
                    .ToList();

                homeViewModel = new HomeViewModel()
                {
                    Flights = cheapestFlights,
                    Jobs = soonestJobs
                };

                this.cacheProvider.InsertWithAbsoluteExpiration(GlobalConstants.HomeCache, homeViewModel, TimeProvider.Current.GetDate().AddMinutes(GlobalConstants.HomeCacheExpiration));
            }

            return View(homeViewModel);
        }
    }
}