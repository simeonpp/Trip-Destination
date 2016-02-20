namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Common.Infrastructure.Constants;
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class CarAdminViewModel : IMapFrom<Car>, IHaveCustomMappings
    {
        private readonly ICarProvider carProvider;

        public CarAdminViewModel()
        {
            this.carProvider = new CarProvider();
        }

        public int Id { get; set; }

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
        public string CarModel { get; set; }

        [Required]
        [MinLength(ModelConstants.CarColorMinLength, ErrorMessage = "Car color can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarColorMaxLength, ErrorMessage = "Car color can no be more than 50 symbols long.")]
        public string Color { get; set; }

        [Range(ModelConstants.CarYearMinYear, ModelConstants.CarYearMaxYear, ErrorMessage = "Car year is not in range.")]
        public int? Year { get; set; }

        [Required]
        [EnumDataType(typeof(SpaceForLugage))]
        public SpaceForLugage SpaceForLugage { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [MinLength(ModelConstants.CarDescriptionMinLength, ErrorMessage = "Car description can no be less than 20 symbols long.")]
        [MaxLength(ModelConstants.CarDescriptionMaxLength, ErrorMessage = "Car description can no be more than 1000 symbols long.")]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> AvailableSeatsSelectList
        {
            get
            {
                return this.carProvider.GetAvailableSeatsSelectList();
            }
        }

        public IEnumerable<SelectListItem> AvailableLuggageSelectList
        {
            get
            {
                return this.carProvider.GetAvailableLuggageSelectList();
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Car, CarAdminViewModel>("CarModel")
                 .ForMember(x => x.CarModel, opt => opt.MapFrom(x => x.Model));
        }
    }
}