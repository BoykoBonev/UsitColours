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
    public class HomeController : Controller
    {
        private readonly IFlightService flightService;
        private readonly IMappingService mappingService;

        public HomeController(IFlightService flightService, IMappingService mappingService)
        {
            if (flightService == null)
            {
                throw new NullReferenceException("FlightService");
            }

            if (mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            this.mappingService = mappingService;
            this.flightService = flightService;
        }

        public ActionResult Index()
        {
            var cheapestFlights = this.flightService.GetCheapestFlights()
                .Select(f => mappingService.Map<FlightVIewModel>(f))
                .ToList();

            var viewModel = new HomeViewModel()
            {
                Flights = cheapestFlights
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}