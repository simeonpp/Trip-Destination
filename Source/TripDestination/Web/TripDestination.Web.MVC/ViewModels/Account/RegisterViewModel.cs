namespace TripDestination.Web.MVC.ViewModels.Account
{
    using Common.Infrastructure.Constants;
    using Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System;

    public class RegisterViewModel : IValidatableObject
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(ModelConstants.UserFirstNameMinLength, ErrorMessage = "User first name can not be less than 3 symbols long.")]
        [MaxLength(ModelConstants.UserFirstNameMaxLength, ErrorMessage = "User first name can not be more than 50 symbols long.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ModelConstants.UserLastNameMinLength, ErrorMessage = "User last name can not be less than 3 symbols long.")]
        [MaxLength(ModelConstants.UserLastNameMaxLength, ErrorMessage = "User last name can not be more than 50 symbols long.")]
        public string LastName { get; set; }

        [AllowHtml]
        [MinLength(ModelConstants.UserDescriptionMinLength, ErrorMessage = "Description can not be less than 20 symbols long.")]
        [MaxLength(ModelConstants.UserDescriptionMaxLength, ErrorMessage = "Description can not be more than 1000 symbols long.")]
        public string Description { get; set; }

        [Required]
        public string Role { get; set; }

        public virtual string PhoneNumber { get; set; }

        public IEnumerable<SelectListItem> AvailableRolesSelectList { get; set; }

        public IEnumerable<SelectListItem> AvailableSeatsSelectList { get; set; }

        public IEnumerable<SelectListItem> AvailableLuggageSelectList { get; set; }

        // Driver (Car)
        [Range(ModelConstants.CarTotalSeatsMinLength, ModelConstants.CarTotalSeatsMaxLength, ErrorMessage = "Car total seat must be between 1 and 5.")]
        public int CarTotalSeats { get; set; }

        [MinLength(ModelConstants.CarBrandMinLength, ErrorMessage = "Car brand can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarBrandMaxLength, ErrorMessage = "Car brand can no be more than 50 symbols long.")]
        public string CarBrand { get; set; }

        [MinLength(ModelConstants.CarModelMinLength, ErrorMessage = "Car model can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarModelMaxLength, ErrorMessage = "Car model can no be more than 50 symbols long.")]
        public string CarModel { get; set; }

        [MinLength(ModelConstants.CarColorMinLength, ErrorMessage = "Car color can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarColorMaxLength, ErrorMessage = "Car color can no be more than 50 symbols long.")]
        public string CarColor { get; set; }

        [Range(ModelConstants.CarYearMinYear, ModelConstants.CarYearMaxYear, ErrorMessage = "Car year is not in range.")]
        public int CarYear { get; set; }

        public SpaceForLugage CarSpaceForLugage { get; set; }

        [AllowHtml]
        [MinLength(ModelConstants.UserDescriptionMinLength, ErrorMessage = "Description can not be less than 20 symbols long.")]
        [MaxLength(ModelConstants.UserDescriptionMaxLength, ErrorMessage = "Description can not be more than 1000 symbols long.")]
        public string CarDescription { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Role == RoleConstants.DriverRole && (this.CarTotalSeats > ModelConstants.CarTotalSeatsMaxLength || this.CarTotalSeats < ModelConstants.CarTotalSeatsMinLength))
            {
                yield return new ValidationResult("You must enter car total seats in range.", new[] { "CarTotalSeats" });
            }

            if (this.Role == RoleConstants.DriverRole && string.IsNullOrEmpty(this.CarBrand))
            {
                yield return new ValidationResult("You must enter car brand.", new[] { "CarBrand" });
            }

            if (this.Role == RoleConstants.DriverRole && string.IsNullOrEmpty(this.CarModel))
            {
                yield return new ValidationResult("You must enter car model.", new[] { "CarModel" });
            }

            if (this.Role == RoleConstants.DriverRole && string.IsNullOrEmpty(this.CarColor))
            {
                yield return new ValidationResult("You must enter car color.", new[] { "CarYear" });
            }

            if (this.Role == RoleConstants.DriverRole && (this.CarYear > ModelConstants.CarYearMaxYear || this.CarYear < ModelConstants.CarYearMinYear))
            {
                yield return new ValidationResult("You must enter car year in range.", new[] { "CarYear" });
            }

            int luggageSpaceAsInt = (int)this.CarSpaceForLugage;
            if (this.Role == RoleConstants.DriverRole && !Enum.IsDefined(typeof(SpaceForLugage), luggageSpaceAsInt))
            {
                yield return new ValidationResult("You must choose car luggage space.", new[] { "CarSpaceForLugage" });
            }
        }
    }
}