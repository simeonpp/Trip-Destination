namespace TripDestination.Common.Infrastructure.Constants
{
    using System;

    public class ModelConstants
    {
        // User
        public const int UserFirstNameMinLength = 3;
        public const int UserFirstNameMaxLength = 50;

        public const int UserLastNameMinLength = 3;
        public const int UserLastNameMaxLength = 50;

        public const int UserDescriptionMinLength = 20;
        public const int UserDescriptionMaxLength = 1000;

        // Car
        public const int CarTotalSeatsMinLength = 1;
        public const int CarTotalSeatsMaxLength = 5;

        public const int CarBrandMinLength = 3;
        public const int CarBrandMaxLength = 50;

        public const int CarModelMinLength = 2;
        public const int CarModelMaxLength = 50;

        public const int CarColorMinLength = 3;
        public const int CarColorMaxLength = 50;

        public const int CarYearMinYear = 1900;
        public const int CarYearMaxYear = 2016;

        public const int CarDescriptionMinLength = 20;
        public const int CarDescriptionMaxLength = 1000;
    }
}
