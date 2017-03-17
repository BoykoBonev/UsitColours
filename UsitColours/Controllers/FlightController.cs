using System;
using System.Web.Mvc;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;

namespace UsitColours.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;
        private readonly IMappingService mappingService;

        public FlightController(IFlightService flightService, IMappingService mappingService)
        {
            if(flightService == null)
            {
                throw new NullReferenceException("FlightService");
            }

            if(mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            this.flightService = flightService;
            this.mappingService = mappingService;
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return View("Home");
            }
            int flightId = (int)id;
            var flight = this.flightService.GetDetailedFlight(flightId);

            var mappedFlight = this.mappingService.Map<DetailsFlightViewModel>(flight);


            return View(mappedFlight);
        }
    }
}