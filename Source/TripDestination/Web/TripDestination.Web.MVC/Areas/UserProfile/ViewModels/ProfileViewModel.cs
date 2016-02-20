namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using TripDestination.Web.MVC.ViewModels.Shared;

    public class ProfileViewModel
    {
        public ExtendedUserViewModel User { get; set; }

        public string Role { get; set; }
    }
}