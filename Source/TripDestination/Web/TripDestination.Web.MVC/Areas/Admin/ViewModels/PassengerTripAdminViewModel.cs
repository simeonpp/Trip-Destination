namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using AutoMapper;

    public class PassengerTripAdminViewModel : IMapFrom<PassengerTrip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int TripId { get; set; }

        public bool Approved { get; set; }

        public string Username { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PassengerTrip, PassengerTripAdminViewModel>("Username")
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.UserName));
        }
    }
}