﻿namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Web.MVC.ViewModels.Shared;
    using TripDestination.Data.Models;
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Linq;
    using Common.Infrastructure.Constants;
    using Infrastructure.HtmlSanitizer;

    public class TripDetailedViewModel : IMapFrom<Trip>, IHaveCustomMappings
    {
        private readonly ISanitizer htmlSanitizer;

        public TripDetailedViewModel()
        {
            this.htmlSanitizer = new HtmlSanitizerAdapter();
        }

        public bool CurrectUserIsDriver { get; set; }

        public bool CurrentUserIsWaitingJoinRequest { get; set; }

        public bool CurrentUserIsJoinedTrip { get; set; }

        public bool HasMoreTripComments { get; set; }

        public bool HasMoreUserComments { get; set; }

        public bool CurrentUserLikedTrip { get; set; }

        public int Id { get; set; }

        public TownViewModel From { get; set; }

        public TownViewModel To { get; set; }

        public DateTime DateOfLeaving { get; set; }

        public string FormattedDateOfLeaving
        {
            get
            {
                return this.DateOfLeaving.ToString("d MMMM yyyy HH:mm");
            }
        }

        public string DateOfLeavingFormatted
        {
            get
            {
                return this.DateOfLeaving.ToString("dd MMMM yyyy");
            }
        }

        public string TimeOfLeavingFormatted
        {
            get
            {
                return this.DateOfLeaving.ToString("HH:mm");
            }
        }

        public string PlaceOfLeaving { get; set; }

        public int AvailableSeats { get; set; }

        public TripStatus Status { get; set; }

        public bool PickUpFromAddress { get; set; }

        public string PickUpFromAddressAsString
        {
            get
            {
                return this.PickUpFromAddress ? "Yes" : "No";
            }
        }

        public decimal Price { get; set; }

        public ExtendedUserViewModel Driver { get; set; }

        public DateTime ETA { get; set; }

        public string FormattedETA
        {
            get
            {
                return this.ETA.ToString("HH:mm");
            }
        }

        public int ViewsCount { get; set; }

        public int LikesCount { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription
        {
            get
            {
                return this.htmlSanitizer.Sanitize(this.Description);
            }
        }

        public IEnumerable<PassengerTripViewModel> Passengers { get; set; }

        public IEnumerable<PassengerTripViewModel> PendingApprovePassengers { get; set; }

        public IEnumerable<BaseCommentViewModel> Comments { get; set; }

        public IEnumerable<BaseCommentViewModel> DriverComments { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormatted
        {
            get
            {
                return this.CreatedOn.ToString("dd MMMM yyyy HH:mm");
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Trip, TripDetailedViewModel>("Passengers")
                .ForMember(x => x.Passengers, opt => opt.MapFrom(x => x.Passengers.Any() ? 
                                                                    x.Passengers.Where(p => p.Approved == true && p.IsDeleted == false)
                                                                    :
                                                                    new List<PassengerTrip>()));

            configuration.CreateMap<Trip, TripDetailedViewModel>("Comments")
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.Any() ?
                                                                    x.Comments
                                                                    .Where(c => c.IsDeleted == false)
                                                                    .OrderByDescending(c => c.CreatedOn)
                                                                    .Take(WebApplicationConstants.CommentsOfset)
                                                                    :
                                                                    new List<TripComment>()));

            configuration.CreateMap<Trip, TripDetailedViewModel>("PendingApprovePassengers")
                .ForMember(x => x.PendingApprovePassengers, opt => opt.MapFrom(x => x.Passengers.Any() ? 
                                                                    x.Passengers.Where(p => p.Approved == false && p.IsDeleted == false)
                                                                    :
                                                                    new List<PassengerTrip>()));

            configuration.CreateMap<Trip, TripDetailedViewModel>("ViewsCount")
                .ForMember(x => x.ViewsCount, opt => opt.MapFrom(x => x.Views.Any() ? x.Views.Count() : 0));
        }
    }
}