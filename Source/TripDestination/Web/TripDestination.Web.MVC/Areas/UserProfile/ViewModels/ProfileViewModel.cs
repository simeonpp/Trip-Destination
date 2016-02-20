namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    public class ProfileViewModel
    {
        public UserViewModel User { get; set; }

        public string Role { get; set; }

        public string CurrentUsername { get; set; }
    }
}