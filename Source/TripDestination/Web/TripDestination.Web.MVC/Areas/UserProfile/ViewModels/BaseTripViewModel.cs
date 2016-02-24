namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Mapping;
    using System;
    using AutoMapper;
    using Data.Models;

    public class BaseTripViewModel : IMapFrom<Trip>, IHaveCustomMappings
    {
        public int Id { get; set; }

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

        public string Url
        {
            get
            {
                return string.Format("/Trip/{0}", this.Id);
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Trip, BaseTripViewModel>("TotalPeople")
                .ForMember(x => x.TotalPeople, opt => opt.MapFrom(x => x.Passengers.Count + 1));
        }
    }
}