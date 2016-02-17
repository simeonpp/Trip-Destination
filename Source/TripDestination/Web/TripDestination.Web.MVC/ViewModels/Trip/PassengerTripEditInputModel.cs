namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using AutoMapper;

    public class PassengerTripEditInputModel : IMapFrom<PassengerTrip>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PassengerTrip, PassengerTripEditInputModel>("Username")
                .ForMember(x => x.Username , opt => opt.MapFrom(x => x.User.UserName));

            configuration.CreateMap<PassengerTrip, PassengerTripEditInputModel>("FirstName")
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName));

            configuration.CreateMap<PassengerTrip, PassengerTripEditInputModel>("LastName")
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName));
        }
    }
}