namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System.Collections.Generic;

    public class ExtendedUserViewModel : BaseUserViewModel, IMapFrom<User>
    {
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public ICollection<UserCommentsViewModel> Comments { get; set; }

        public CarViewModel Car { get; set; }
    }
}