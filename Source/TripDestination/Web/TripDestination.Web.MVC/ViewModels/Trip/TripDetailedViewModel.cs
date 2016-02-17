﻿namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Web.MVC.ViewModels.Shared;
    using TripDestination.Data.Models;
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Linq;

    public class TripDetailedViewModel : IMapFrom<Trip>, IHaveCustomMappings
    {
        public bool CurrectUserIsDriver { get; set; }

        public bool CurrentUserIsWaitingJoinRequest { get; set; }

        public int Id { get; set; }

        public Town From { get; set; }

        public Town To { get; set; }

        public DateTime DateOfLeaving { get; set; }

        public string PlaceOfLeaving { get; set; }

        public int AvailableSeats { get; set; }

        public TripStatus Status { get; set; }

        public bool PickUpFromAddress { get; set; }

        public decimal Price { get; set; }

        public User Driver { get; set; }

        public DateTime ETA { get; set; }

        public virtual IEnumerable<View> Views { get; set; }

        public virtual IEnumerable<Like> Likes { get; set; }

        public string Description { get; set; }

        public IEnumerable<PassengerTrip> Passengers { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Trip, TripDetailedViewModel>("Passengers")
                .ForMember(x => x.Passengers, opt => opt.MapFrom(x => x.Passengers.Where(p => p.Approved == true && p.IsDeleted == false)));
        }
    }
}