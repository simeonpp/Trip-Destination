﻿namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using System.Linq;

    public class TripEditInputModel : IMapFrom<Trip>, IHaveCustomMappings
    {
        public string DriverId { get; set; }

        public int Id { get; set; }

        public IEnumerable<SelectListItem> AddressPickUpSelectList { get; set; }

        public IEnumerable<SelectListItem> LeaftAvailabeSeatsSelectList { get; set; }

        public string FromTownName { get; set; }

        public string ToTownName { get; set; }

        [Required]
        [MinLength(ModelConstants.TripPlaceOfLeavingMinLength, ErrorMessage = "Trip place of leaving can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.TripPlaceOfLeavingMaxLength, ErrorMessage = "Trip place of leaving can no be more than 200 symbols long.")]
        public string PlaceOfLeaving { get; set; }

        [Required]
        [Display(Name = "Left available seats")]
        [Range(ModelConstants.TripAvailableSeatsMin, ModelConstants.TripAvailableSeatsMax, ErrorMessage = "Trip available seats should be between 1 and 5")]
        public int LeftAvailableSeats { get; set; }

        [Required]
        public DateTime DateOfLeaving { get; set; }

        public string FormattedDateOfLeaving
        {
            get
            {
                return this.DateOfLeaving.ToString("d MMMM yyyy HH:mm");
            }
        }

        [Required]
        public DateTime ETA { get; set; }

        [Required]
        public bool PickUpFromAddress { get; set; }

        [MinLength(ModelConstants.TripDescriptionMinLength, ErrorMessage = "Trip description can not be less than 10 symbols long.")]
        [MaxLength(ModelConstants.TripDescriptionMaxLength, ErrorMessage = "Trip description can not be more than 1000 symbols long.")]
        [DataType(DataType.MultilineText)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        public ICollection<PassengerTripEditInputModel> Passengers { get; set; }

        public string UsernamesToBeRemoved { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Trip, TripEditInputModel>("Passengers")
                .ForMember(x => x.Passengers, opt => opt.MapFrom(x => x.Passengers
                                                                        .Where(p => p.Approved == true && p.IsDeleted == false)));

            configuration.CreateMap<Trip, TripEditInputModel>("FromTownName")
                .ForMember(x => x.FromTownName, opt => opt.MapFrom(x => x.From.Name));

            configuration.CreateMap<Trip, TripEditInputModel>("ToTownName")
                .ForMember(x => x.ToTownName, opt => opt.MapFrom(x => x.To.Name));
        }
    }
}