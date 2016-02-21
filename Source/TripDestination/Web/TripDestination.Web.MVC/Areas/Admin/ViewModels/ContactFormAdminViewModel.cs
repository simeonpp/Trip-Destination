namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using System;
    using System.Web.Mvc;
    using TripDestination.Data.Models;

    [Bind(Exclude = "CreatedOn")]
    public class ContactFormAdminViewModel : IMapFrom<ContactForm>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Ip { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}