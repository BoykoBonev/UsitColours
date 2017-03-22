using UsitColours.AutoMapper;

namespace UsitColours.Models
{
    public class JobBaseViewModel: IMapFrom<Job>
    {
        public int Id { get; set; }

        public string JobTitle { get; set; }

        public decimal Wage { get; set; }

        public decimal Price { get; set; }

        public string CityName { get; set; }

        public string ImagePath { get; set; }
    }
}