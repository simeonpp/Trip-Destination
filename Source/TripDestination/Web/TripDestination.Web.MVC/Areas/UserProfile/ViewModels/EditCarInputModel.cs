namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Constants;
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;
    using AutoMapper;
    using System;

    public class EditCarInputModel : IMapFrom<Car>, IHaveCustomMappings
    {
        private readonly ICarProvider carProvider;

        public EditCarInputModel()
        {
            this.carProvider = new CarProvider();
            this.AvailableSeatsSelectList = this.carProvider.GetAvailableSeatsSelectList();
            this.AvailableLuggageSelectList = this.carProvider.GetAvailableLuggageSelectList();
        }

        public string Firsname { get; set; }

        public string Lastname { get; set; }

        public BaseEditModel BaseModel { get; set; }

        [Required]
        [Range(ModelConstants.CarTotalSeatsMinLength, ModelConstants.CarTotalSeatsMaxLength, ErrorMessage = "Car total seat must be between 1 and 5.")]
        public int TotalSeats { get; set; }

        [Required]
        [MinLength(ModelConstants.CarBrandMinLength, ErrorMessage = "Car brand can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarBrandMaxLength, ErrorMessage = "Car brand can no be more than 50 symbols long.")]
        public string Brand { get; set; }

        [Required]
        [MinLength(ModelConstants.CarModelMinLength, ErrorMessage = "Car model can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarModelMaxLength, ErrorMessage = "Car model can no be more than 50 symbols long.")]
        public string CarModel { get; set; } // With only Model ASP.MVC has problem when trying to bind data

        [Required]
        [MinLength(ModelConstants.CarColorMinLength, ErrorMessage = "Car color can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarColorMaxLength, ErrorMessage = "Car color can no be more than 50 symbols long.")]
        public string Color { get; set; }

        [Range(ModelConstants.CarYearMinYear, ModelConstants.CarYearMaxYear, ErrorMessage = "Car year is not in range.")]
        public int? Year { get; set; }

        [Required]
        public SpaceForLugage SpaceForLugage { get; set; }

        [AllowHtml]
        [MinLength(ModelConstants.CarDescriptionMinLength, ErrorMessage = "Car description can no be less than 20 symbols long.")]
        [MaxLength(ModelConstants.CarDescriptionMaxLength, ErrorMessage = "Car description can no be more than 1000 symbols long.")]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> AvailableSeatsSelectList { get; set; }

        public IEnumerable<SelectListItem> AvailableLuggageSelectList { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Car, EditCarInputModel>("CarModel")
                .ForMember(x => x.CarModel, opt => opt.MapFrom(x => x.Model));
        }
    }
}