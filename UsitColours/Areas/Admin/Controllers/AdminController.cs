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

        private readonly IMappingService mappingService;

        public AdminController(ICountryService countryServices, IMappingService mappingService, ICityService cityService)
        {
            if(countryServices == null)
            {
                throw new NullReferenceException("CountryService");
            }

            if(mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            if(cityService == null)
            {
                throw new NullReferenceException("CityService");
            }

            this.cityService = cityService;
            this.mappingService = mappingService;
            this.countryServices = countryServices;
        }
  
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCountry()
        {
            return View();
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
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString()})
                .ToList();

            var viewModel = new AddCityViewModel()
            {
                Countries = countries
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCity(CityViewModel cityViewModel)
        {
            if(!this.ModelState.IsValid)
            {
                return this.View(cityViewModel);
            }

            City city = mappingService.Map<City>(cityViewModel);
            this.cityService.AddCity(city);

            this.TempData["Notification"] = "Exchange information between two requests";
            return View("Index");
        }


    }
}