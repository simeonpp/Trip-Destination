namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    public class BaseEditModel : IMapFrom<User>
    {
        private readonly IMediaImageUrlProvider imageUrlProvider;

        public BaseEditModel()
        {
            this.imageUrlProvider = new MediaImageUrlProvider();
        }

        public string Role { get; set; }

        public string CurrentUsername { get; set; }

        [Required]
        [MinLength(ModelConstants.UserFirstNameMinLength, ErrorMessage = "User first name can not be less than 3 symbols long.")]
        [MaxLength(ModelConstants.UserFirstNameMaxLength, ErrorMessage = "User first name can not be more than 50 symbols long.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ModelConstants.UserLastNameMinLength, ErrorMessage = "User last name can not be less than 3 symbols long.")]
        [MaxLength(ModelConstants.UserLastNameMaxLength, ErrorMessage = "User last name can not be more than 50 symbols long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public virtual string PhoneNumber { get; set; }

        [AllowHtml]
        [MinLength(ModelConstants.UserDescriptionMinLength, ErrorMessage = "Description can not be less than 20 symbols long.")]
        [MaxLength(ModelConstants.UserDescriptionMaxLength, ErrorMessage = "Description can not be more than 1000 symbols long.")]
        public string Description { get; set; }

        public string AvatarFilename { get; set; }

        public string AvatarUrlSmall
        {
            get
            {
                return this.imageUrlProvider.GetImageUrl(this.AvatarFilename, WebApplicationConstants.ImageUserAvatarSmallWidth);
            }
        }
    }
}