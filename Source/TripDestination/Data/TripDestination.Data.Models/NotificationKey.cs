namespace TripDestination.Data.Models
{
    public enum NotificationKey
    {
        // Code 1 - User related
        AdminRoleRequest = 11,
        ApprovelAdminRequest = 12,
        RejectAdminRequest = 13,

        // Code 2 - Trip related
        JointTripRequest = 21,
        CloseTripDriverRequest = 22,
        TripFinished = 23,
        TripChanged = 24,
        DriverRemovePassenger = 25,
        PassengerLeftTrip = 26
    }
}
