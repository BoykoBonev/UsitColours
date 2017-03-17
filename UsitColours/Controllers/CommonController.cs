using System;
using System.Linq;
using System.Web.Mvc;
using UsitColours.Areas.Admin.Models;
using UsitColours.AutoMapper;
using UsitColours.Services.Contracts;

namespace UsitColours.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICityService cityService;
        private readonly IMappingService mappingService;
        private readonly IAirportService airportService;

        public CommonController(IMappingService mappingService, ICityService cityService, IAirportService airportService)
        {
            if (mappingService == null)
            {
                throw new NullReferenceException("MappingService");
            }

            if (cityService == null)
            {
                throw new NullReferenceException("CityService");
            }

            if (airportService == null)
            {
                throw new NullReferenceException("AirportService");
            }

          
            this.cityService = cityService;
            this.mappingService = mappingService;
            this.airportService = airportService;
        }

        protected ICityService CityService
        {
            get
            {
                return this.cityService;
            }
        }

        protected IMappingService MappingService
        {
            get
            {
                return this.mappingService ;
            }
        }

        protected IAirportService AirportService
        {
            get
            {
                return this.airportService;
            }
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