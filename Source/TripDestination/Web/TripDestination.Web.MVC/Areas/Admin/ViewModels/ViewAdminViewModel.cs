namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Data.Models;
    using System;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Mapping;

    [Bind(Exclude = "CreatedOn")]
    public class ViewAdminViewModel : IMapFrom<View>
    {
        public int Id { get; set; }

        public int TripId { get; set; }

        public string Ip { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}