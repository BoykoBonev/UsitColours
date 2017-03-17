using System;
using System.Linq;
using System.Web.Mvc;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Controllers
{
    public class FlightController : CommonController
    {
        private readonly IFlightService flightService;
        private readonly ICountryService countryService;

        public FlightController(IFlightService flightService, IMappingService mappingService, ICountryService countryService, IAirportService airportService, ICityService cityService)
            : base(mappingService, cityService, airportService )
        {
            if(flightService == null)
            {
                throw new NullReferenceException("FlightService");
            }

            if (countryService == null)
            {
                throw new NullReferenceException("CountryService");
            }

            this.countryService = countryService;
            this.flightService = flightService;
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return View("Home");
            }
            int flightId = (int)id;
            var flight = this.flightService.GetDetailedFlight(flightId);

            var mappedFlight = base.MappingService.Map<DetailsFlightViewModel>(flight);

            return View(mappedFlight);
        }


        public ActionResult Search()
        {
            var countries = this.countryService.GetAllCountries()
             .Select(x => MappingService.Map<CountryViewModel>(x))
             .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() })
             .ToList();

            var viewModel = new SearchViewModel()
            {
                Countries = countries
            };

            return View(viewModel);
        }
    }
}