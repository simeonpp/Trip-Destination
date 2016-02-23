namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Web.Mvc;

    [Bind(Exclude = "CreatedOn")]
    public class NewsletterAdminViewModel : IMapFrom<Newsletter>
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Ip { get; set; }

        public string UserAgent { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}