namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;

    public class NotificationTypeAdminViewModel : IMapFrom<NotificationType>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public NotificationKey Key { get; set; }
    }
}