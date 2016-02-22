namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class TripNotificationViewModel : IMapFrom<TripNotification>
    {
        //public BaseTripModel Trip { get; set; }

        public BaseUserViewModel FromUser { get; set; }

        public bool Seen { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}