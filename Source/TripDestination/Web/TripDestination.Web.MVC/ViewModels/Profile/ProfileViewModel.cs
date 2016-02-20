namespace TripDestination.Web.MVC.ViewModels.Profile
{
    using TripDestination.Web.MVC.ViewModels.Shared;

    public class ProfileViewModel
    {
        public ExtendedUserViewModel User { get; set; }

        public string Role { get; set; }
    }
}