namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class BaseUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}