namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;
    using System.Collections.Generic;
    using AutoMapper;

    [Bind(Exclude = "DateOfLeaving,ETA")]
    public class TripAdminViewModel : IMapFrom<Trip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime DateOfLeaving { get; set; }

        [Display(Name = "From")]
        public string FromName { get; set; }

        [Display(Name = "To")]
        public string ToName { get; set; }

        public DateTime ETA { get; set; }

        [Required]
        [MinLength(ModelConstants.TripPlaceOfLeavingMinLength, ErrorMessage = "Trip place of leaving can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.TripPlaceOfLeavingMaxLength, ErrorMessage = "Trip place of leaving can no be more than 200 symbols long.")]
        public string PlaceOfLeaving { get; set; }

        [Required]
        public bool PickUpFromAddress { get; set; }

        [Required]
        [Range(ModelConstants.TripAvailableSeatsMin, ModelConstants.TripAvailableSeatsMax, ErrorMessage = "Trip available seats should be between 1 and 5")]
        public int AvailableSeats { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public TripStatus Status { get; set; }

        [MinLength(ModelConstants.TripDescriptionMinLength, ErrorMessage = "Trip description can not be less than 10 symbols long.")]
        [MaxLength(ModelConstants.TripDescriptionMaxLength, ErrorMessage = "Trip description can not be more than 1000 symbols long.")]
        public string Description { get; set; }

        public List<SelectListItem> AvailabeSeatsSelectList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "1", Value = "1" },
                    new SelectListItem { Text = "2", Value = "2" },
                    new SelectListItem { Text = "3", Value = "3" },
                    new SelectListItem { Text = "4", Value = "4" }
                };
            }
        }

        public List<SelectListItem> PickUpSelectList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Yes", Value = "true" },
                new SelectListItem { Text = "No", Value = "false" }
                };
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Trip, TripAdminViewModel>("FromName")
                .ForMember(x => x.FromName, opt => opt.MapFrom(x => x.From.Name));

            configuration.CreateMap<Trip, TripAdminViewModel>("ToName")
                .ForMember(x => x.ToName, opt => opt.MapFrom(x => x.To.Name));
        }
    }
}