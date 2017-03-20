using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UsitColours.AutoMapper;
using UsitColours.Models;
using UsitColours.Services.Contracts;
using UsitColours.Services.Models;

namespace UsitColours.Controllers
{
    public class TicketController : BaseController
    {
        private readonly IUserService userService;
        private readonly IMappingService mappingService;

        public TicketController(IUserService userService, IMappingService mappingService)
        {
            if (userService == null)
            {
                throw new NullReferenceException("UserService");
            }

            if(mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            this.userService = userService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            var flights = (IEnumerable<DetailsFlightViewModel>)this.TempData["Ticket"];

            return View(flights);
        }

        [Authorize]
        public ActionResult Buy()
        {
            string currentUserId = User.Identity.GetUserId();

            var flights = (IEnumerable<DetailsFlightViewModel>)this.TempData["Ticket"];

            var presentationFlights = flights
                .Select(f => this.mappingService.Map<PresentationFlight>(f))
                .ToList();

            var hasEnoughMoney = this.userService.BuyTicket(currentUserId, presentationFlights);

            if(hasEnoughMoney)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // TODO- Message for fail transaction
                return View("Index");
            }
        }
    }
}