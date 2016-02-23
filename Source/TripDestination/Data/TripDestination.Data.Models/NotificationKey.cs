namespace TripDestination.Data.Models
{
    public enum NotificationKey
    {
        // Code 2 - Trip related
        JoinTripRequest = 21,
        JoinTripApproved = 22,
        JoinTripDisApproved = 23,
        FinishTripDriverRequest = 24,
        TripFinished = 25,
        TripChanged = 26,
        DriverRemovePassenger = 27,
        PassengerLeftTrip = 28
    }
}
