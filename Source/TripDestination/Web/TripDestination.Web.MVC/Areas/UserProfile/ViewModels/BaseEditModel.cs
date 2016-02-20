namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;

    public class BaseEditModel : IMapFrom<User>
    {
        private readonly IMediaImageUrlProvider imageUrlProvider;

        public BaseEditModel()
        {
            this.imageUrlProvider = new MediaImageUrlProvider();
        }

        public string Role { get; set; }

        public string CurrentUsername { get; set; }

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