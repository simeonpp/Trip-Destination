﻿namespace TripDestination.Common.Infrastructure.Constants
{
    public class NotificationConstants
    {
        public const string JoinTripRequestTitle = "Join trip request";
        public const string JoinTripRequestMessageFormat = "{0} would like to join your trip from {1} to {2} on {3}";

        public const string PassengerLeftTripTitle = "Passenger left trip";
        public const string PassengerLeftTripMessageFormat = "{0} left your trip from {1} to {2} on {3}";

        public const string TripChangedTitle = "The trip you are joined was changed.";
        public const string TripChangedpMessageFormat = "{0} changed trip from {1} to {2} on {3}";

        public const string DriverRemovePassegerTitle = "You were removed from trip";
        public const string DriverRemovePassegerFormat = "{0} remove you from trip {1} to {2} on {3}";

        public const string TripRequestDisaprovedTitle = "Your trip request was rejected";
        public const string TripRequestDisaprovedFormat = "{0} reject you trip request from {1} to {2} on {3}";

        public const string TripRequestApprovedTitle = "Your trip request was approved";
        public const string TripRequestApprovedFormat = "{0} approved you trip request from {1} to {2} on {3}";

        public const int CloseTripDriverRequestAvaialableDaysAfterTripFinished = 10;
        public const string CloseTripDriverRequestTitle = "Close your trip";
        public const string CloseTripDriverRequestFormat = "Please close your trip from {0} to {1} on {2}";

        public const string TripFinishTitle = "Trip finished.";
        public const string TripFinishRequestPassengerFormat = "Please rate the driver ({0}) of trip: {1} - {2} on {3}";
        public const string TripFinishRequestDriverFormat = "Please rate your passengers from trip {0} - {1} on {2}";
    }
}
