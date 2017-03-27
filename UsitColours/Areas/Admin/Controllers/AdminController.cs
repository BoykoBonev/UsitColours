using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : CommonController
    {
        private readonly ICountryService countryServices;
        private readonly IAirlineService airlineService;
        private readonly IFlightService flightService;
        private readonly IJobService jobService;

        public AdminController(ICountryService countryServices, IMappingService mappingService, ICityService cityService, IAirlineService airlineService, IAirportService airportService, IFlightService flightService, IJobService jobService)
            : base(mappingService, cityService, airportService)
        {
            if (countryServices == null)
            {
                throw new NullReferenceException("CountryService");
            }

            if (airlineService == null)
            {
                throw new NullReferenceException("AirlineService");
            }

            if (flightService == null)
            {
                throw new NullReferenceException("FlightService");
            }

            if (jobService == null)
            {
                throw new NullReferenceException("JobService");
            }

            this.jobService = jobService;
            this.flightService = flightService;
            this.airlineService = airlineService;
            this.countryServices = countryServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCountry()
        {
            return PartialView("_AddCountry");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCountry(string name)
        {
            this.countryServices.AddCountry(name);
            return View("Index");
        }

        public ActionResult AddCity()
        {

            var countries = this.countryServices.GetAllCountries()
                .AsQueryable()
                .Select(x => MappingService.Map<CountryViewModel>(x))
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() })
                .ToList();

            var viewModel = new AddCityViewModel()
            {
                Countries = countries
            };

            return PartialView("_AddCity", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCity(CityViewModel cityViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(AdminController.Index));
            }

            City city = base.MappingService.Map<City>(cityViewModel);
            base.CityService.AddCity(city);

            this.TempData["Notification"] = "City successfully added";
            return View("Index");
        }


        public ActionResult AddAirline()
        {
            return PartialView("_AddAirline");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAirline(string name)
        {
            this.airlineService.AddAirline(name);
            return View("Index");
        }


        public ActionResult AddAirport()
        {
            var countries = this.countryServices.GetAllCountries()
              .AsQueryable()
              .Select(x => MappingService.Map<CountryViewModel>(x))
              .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() })
              .ToList();

            var viewModel = new AddAirportViewModel()
            {
                Countries = countries
            };

            return PartialView("_AddAirport", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAirport(AirportViewModel airport)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(AdminController.Index));
            }

            var airportModel = base.MappingService.Map<Airport>(airport);

            base.AirportService.AddAirport(airportModel);
            return View("Index");
        }

        public ActionResult AddFlight()
        {
            var countries = this.countryServices.GetAllCountries()
              .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() })
              .ToList();

            var airlines = this.airlineService.GetAllAirlines()
              .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString() })
              .ToList();

            var viewModel = new AddFlightViewModel()
            {
                Countries = countries,
                Airlines = airlines
            };

            return PartialView("_AddFlight", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFlight(FlightModel flight)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(AdminController.Index));
            }

            var flightModel = base.MappingService.Map<Flight>(flight);

            this.flightService.AddFlight(flightModel);

            return View("Index");
        }

        public ActionResult AddJob()
        {
            var countries = this.countryServices.GetAllCountries()
              .AsQueryable()
              .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() })
              .ToList();


            var viewModel = new AddJobViewModel()
            {
                Countries = countries,
            };

            return PartialView("_AddJob", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJob(JobViewModel job, HttpPostedFileBase file)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(AdminController.Index));
            }

            string path = string.Empty;
            if (job.IsDefaultImage)
            {
                path = System.IO.Path.Combine(
                                 Server.MapPath("~/images"), "job-default.jpg");
            }
            else
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                path = System.IO.Path.Combine(
                                 Server.MapPath("~/images"), pic);

                file.SaveAs(path);
            }

            job.ImagePath = path;

            var jobModel = base.MappingService.Map<Job>(job);

            this.jobService.AddJob(jobModel);

            return View("Index");
        }
    }
}