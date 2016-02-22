namespace TripDestination.Data.Models
{
    public enum NotificationKey
    {
        // Code 1 - User related
        AdminRoleRequest = 11,
        ApprovelAdminRequest = 12,
        RejectAdminRequest = 13,

        // Code 2 - Trip related
        JoinTripRequest = 21,
        JoinTripApproved = 22,
        JoinTripDisApproved = 23,
        CloseTripDriverRequest = 24,
        TripFinished = 25,
        TripChanged = 26,
        DriverRemovePassenger = 27,
        PassengerLeftTrip = 28
    }
}
