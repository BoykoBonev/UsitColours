using System;
using System.Linq;
using System.Web.Mvc;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICountryService countryServices;
        private readonly ICityService cityService;
        private readonly IAirlineService airlineService;
        private readonly IMappingService mappingService;
        private readonly IAirportService airportService;
        private readonly IFlightService flightService;

        public AdminController(ICountryService countryServices, IMappingService mappingService, ICityService cityService, IAirlineService airlineService, IAirportService airportService, IFlightService flightService)
        {
            if (countryServices == null)
            {
                throw new NullReferenceException("CountryService");
            }

            if (mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            if (cityService == null)
            {
                throw new NullReferenceException("CityService");
            }

            if (airlineService == null)
            {
                throw new NullReferenceException("AirlineService");

            }

            if (airportService == null)
            {
                throw new NullReferenceException("AirportService");
            }

            if (flightService == null)
            {
                throw new NullReferenceException("FlightService");
            }

            this.flightService = flightService;
            this.airlineService = airlineService;
            this.cityService = cityService;
            this.mappingService = mappingService;
            this.countryServices = countryServices;
            this.airportService = airportService;
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
                .Select(x => mappingService.Map<CountryViewModel>(x))
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

            City city = mappingService.Map<City>(cityViewModel);
            this.cityService.AddCity(city);

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
              .Select(x => mappingService.Map<CountryViewModel>(x))
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

            var airportModel = this.mappingService.Map<Airport>(airport);

            this.airportService.AddAirport(airportModel);
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

            var flightModel = this.mappingService.Map<Flight>(flight);

            this.flightService.AddFlight(flightModel);

            return View("Index");
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadCities(string countryId)
        {
            int id = int.Parse(countryId);
            var cities = this.cityService.GetCityInCountry(id)
                 .AsQueryable()
                .Select(x => mappingService.Map<CityViewModel>(x))
                .ToList();


            var citiesList = cities.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString()
            });

            return Json(citiesList, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadAirports(string cityId)
        {
            int id = int.Parse(cityId);

            var airports = this.airportService.GetAllAirportsInCity(id)
                 .AsQueryable()
                .Select(x => mappingService.Map<AirportViewModel>(x))
                .ToList();


            var airportsList = airports.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString()
            });

            return Json(airports, JsonRequestBehavior.AllowGet);
        }



    }
}