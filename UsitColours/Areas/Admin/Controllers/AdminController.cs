using System;
using System.Linq;
using System.Web.Mvc;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Areas.Admin.Controllers
{
    public class AdminController : CommonController
    {
        private readonly ICountryService countryServices;
        private readonly IAirlineService airlineService;
        private readonly IFlightService flightService;

        public AdminController(ICountryService countryServices, IMappingService mappingService, ICityService cityService, IAirlineService airlineService, IAirportService airportService, IFlightService flightService)
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
        public ActionResult AddCity(CityViewModel cityViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(cityViewModel);
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
        public ActionResult AddAirport(AirportViewModel airport)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(airport);
            }

            var airportModel = base.MappingService.Map<Airport>(airport);

            base.AirportService.AddAirport(airportModel);
            return View("Index");
        }


        public ActionResult AddFlight()
        {
            var countries = this.countryServices.GetAllCountries()
              .AsQueryable()
              .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() })
              .ToList();

            var airlines = this.airlineService.GetAllAirlines()
                .AsQueryable()
              .Select(a => new SelectListItem() { Text = a.Name, Value = a.Id.ToString() })
              .ToList();

            var viewModel = new AddFlightViewModel()
            {
                Countries = countries,
                Airlines =airlines
            };

            return PartialView("_AddFlight", viewModel);
        }

        [HttpPost]
        public ActionResult AddFlight(FlightModel flight)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(flight);
            }

            var flightModel = base.MappingService.Map<Flight>(flight);

            this.flightService.AddFlight(flightModel);

            return View("Index");
        }

       
    }
}