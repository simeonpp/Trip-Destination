namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using System.Collections.Generic;
    using TripDestination.Web.MVC.ViewModels.Shared;

    public class ProfileViewModel
    {
        public UserViewModel User { get; set; }

        public string Role { get; set; }

        public string CurrentUsername { get; set; }

        public IEnumerable<BaseCommentViewModel> Comments { get; set; }

        public int TotalComments { get; set; }

        public bool HasMoreComments { get; set; }
    }
}