namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;

    public class TripNotificationAdminViewModel : IMapFrom<TripNotification>
    {
        public int TypeId { get; set; }
    }
}