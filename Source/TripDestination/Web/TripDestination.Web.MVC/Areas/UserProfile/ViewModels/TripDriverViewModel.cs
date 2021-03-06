﻿namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;

    public class TripDriverViewModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileUrl
        {
            get
            {
                return string.Format("/User/{0}/{1}", this.UserName, this.FirstName + "-" + this.LastName);
            }
        }
    }
}