namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class TownViewModel : IMapFrom<Town>
    {
        public string  Name { get; set; }
    }
}