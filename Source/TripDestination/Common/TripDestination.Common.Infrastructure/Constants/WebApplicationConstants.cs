using System.Collections.Generic;

namespace TripDestination.Common.Infrastructure.Constants
{
    public class WebApplicationConstants
    {
        public const int HomepageTripsPerSectionCount = 4;

        public const string TempDataKeyForContactForm = "ContactFormSuccessfullySend";

        public const int CommentsOfset = 3;

        public const int ImageQuality = 70;
        public const int ImageUserAvatarNormalWidth = 303;
        public const int ImageUserAvatarSmallWidth = 85;
        public const int ImageCarWidth = 482;
        public const int ImageMaxSizeInBytes = 1572864; // 1.5MB
        public const string ImageFolder = "~/App_Data/uploads";
        public const string ImageRouteUrl = "Media/Image";

        public const int MaxItemsPerPage = 24;

        // Ordering
        private static Dictionary<string, string> orderTripOptions = new Dictionary<string, string>()
        {
            { "DateOfLeaving", "date of leaving" },
            { "AvailableSeats", "available seats" },
            { "Price", "price" },
            { "Driver", "driver name" }
        };

        public static Dictionary<string, string> OrderTripOptions
        {
            get
            {
                return orderTripOptions;
            }
        }
    }
}
