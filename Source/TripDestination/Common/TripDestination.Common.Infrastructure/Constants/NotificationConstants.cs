namespace TripDestination.Common.Infrastructure.Constants
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
    }
}
