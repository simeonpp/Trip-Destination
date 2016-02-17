namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;

    public class UserCommentsViewModel : IMapFrom<UserComment>
    {
        public BaseUserViewModel Author { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}