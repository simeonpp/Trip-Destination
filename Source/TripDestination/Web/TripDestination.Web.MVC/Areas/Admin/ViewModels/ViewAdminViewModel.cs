namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Glimpse.Mvc.AlternateType;
    using System;
    using TripDestination.Common.Infrastructure.Mapping;

    public class ViewAdminViewModel : IMapFrom<View>
    {
        public int Id { get; set; }

        public int TripId { get; set; }

        public string Ip { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}