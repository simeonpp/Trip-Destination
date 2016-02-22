namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class NotificationTripViewModel : IMapFrom<TripNotification>
    {
        public int Id { get; set; }

        public TripViewModelForNotification Trip { get; set; }

        public NotificationTypeViewModel Type { get; set; }

        public bool Seen { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string MessageFormatted
        {
            get
            {
                string message = this.Message.Length > 40 ? this.Message.Substring(0, 39) + "..." : this.Message;
                return message;
            }
        }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormatted
        {
            get
            {
                return this.CreatedOn.ToString("dd.MM.yyyy HH:mm");
            }
        }

        public string StyleClassName
        {
            get
            {
                string styleClassName = this.Seen ? "seen" : "notSeen";
                return styleClassName;
            }
        }
    }
}