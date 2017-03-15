using UsitColours.AutoMapper;
using UsitColours.Models;

namespace UsitColours.Areas.Admin.Models
{
    public class CountryViewModel: IMapTo<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}