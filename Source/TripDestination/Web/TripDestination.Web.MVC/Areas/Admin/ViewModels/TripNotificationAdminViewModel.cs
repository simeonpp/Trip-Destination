namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using AutoMapper;

    public class TripNotificationAdminViewModel : IMapFrom<TripNotification>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FromUserName { get; set; }

        public string ForUserName { get; set; }

        public bool Seen { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public bool ActionHasBeenTaken { get; set; }

        public int TripId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<TripNotification, TripNotificationAdminViewModel>("FromUserName")
                .ForMember(x => x.FromUserName, opt => opt.MapFrom(x => x.FromUser.UserName));

            configuration.CreateMap<TripNotification, TripNotificationAdminViewModel>("ForUserName")
                .ForMember(x => x.ForUserName, opt => opt.MapFrom(x => x.ForUser.UserName));
        }
    }
}