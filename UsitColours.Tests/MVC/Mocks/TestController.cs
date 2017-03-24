using UsitColours.AutoMapper;
using UsitColours.Controllers;
using UsitColours.Services.Contracts;

namespace UsitColours.Tests.MVC.Mocks
{
    public class TestController : CommonController
    {
        public TestController(IMappingService mappingService, ICityService cityService, IAirportService airportService) : base(mappingService, cityService, airportService)
        {
        }

        public IMappingService GetMappingService
        {
            get
            {
                return base.MappingService;
            }
        }

        public ICityService GetCityService
        {
            get
            {
                return base.CityService;
            }
        }

        public IAirportService GetAirportService
        {
            get
            {
                return base.AirportService;
            }
        }
    }
}
