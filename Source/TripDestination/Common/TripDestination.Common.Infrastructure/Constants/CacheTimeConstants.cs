﻿namespace TripDestination.Common.Infrastructure.Constants
{
    public class CacheTimeConstants
    {
        // Layout
        public const int NavigationPages = 60 * 60 * 60;

        // Home
        public const int HomeTopDestination = 60 * 15;
        public const int HomeTodayTrips = 60 * 5;
        public const int HomeLatestTrips = 60 * 2;

        // Statistics
        public const int StatisticsTodaysTrips = 60 * 60 * 15;

        // User profile
        public const int UserProfileStatistics = 60 * 60 * 60;

        // Towns
        public const int Towns = 60 * 60 * 60 * 5; // 5 hours
    }
}
