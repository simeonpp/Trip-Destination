namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class PhotoViewModel : IMapFrom<Photo>
    {
        private readonly IMediaImageUrlProvider imageUrlProvider;

        public PhotoViewModel()
        {
            this.imageUrlProvider = new MediaImageUrlProvider();
        }

        public string ContentType { get; set; }

        public string Extension { get; set; }

        public string OriginalName { get; set; }

        public int SizeInBytes { get; set; }

        public string FileName { get; set; }

        public string ImageUrl
        {
            get
            {
                return this.imageUrlProvider.GetImageUrl(this.FileName);
            }
        }
    }
}