using System;

namespace UsitColours.Constants
{
    public static class GlobalConstants
    {
        public const int JobTitleMinLength = 2;
        public const int JobTitleMaxLength = 50;

        public const int JobDescriptionMinLength = 10;
        public const int JobDescriptionMaxLength = 100000;

        public const int CompanyMinLength = 2;
        public const int CompanyMaxLength = 100;

        public const int CountryMinLength = 2;
        public const int CountryMaxLength = 100;

        public const int CityMinLength = 2;
        public const int CityMaxLength = 100;

        public const int AirportMinLength = 2;
        public const int AirportMaxLength = 100;

        public const int AirlineMinLength = 2;
        public const int AirlineMaxLength = 100;

        public const string HomeCache = "HomeCache";
        public const int HomeCacheExpiration = 10;
    }
}