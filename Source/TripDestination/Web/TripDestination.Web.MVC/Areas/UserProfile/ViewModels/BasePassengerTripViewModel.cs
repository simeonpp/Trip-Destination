namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using AutoMapper;

    public class BasePassengerTripViewModel : IMapFrom<PassengerTrip>, IHaveCustomMappings
    {
        public DateTime DateOfLeaving { get; set; }

        public string DateOfLeavingFormate
        {
            get
            {
                return this.DateOfLeaving.ToString("dd.MM.yyyy HH:mm");
            }
        }

        public TripDriverViewModel Driver { get; set; }

        public int TotalPeople { get; set; }

        public TripStatus Status { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PassengerTrip, BasePassengerTripViewModel>("DateOfLeaving")
                .ForMember(x => x.DateOfLeaving, opt => opt.MapFrom(x => x.Trip.DateOfLeaving));

            configuration.CreateMap<PassengerTrip, BasePassengerTripViewModel>("Driver")
                .ForMember(x => x.Driver, opt => opt.MapFrom(x => x.Trip.Driver));

            configuration.CreateMap<PassengerTrip, BasePassengerTripViewModel>("TotalPeople")
                .ForMember(x => x.TotalPeople, opt => opt.MapFrom(x => x.Trip.Passengers.Count + 1));

            configuration.CreateMap<PassengerTrip, BasePassengerTripViewModel>("Status")
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Trip.Status));
        }
    }
}