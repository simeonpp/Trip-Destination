﻿namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Constants;
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

        public string FileName { get; set; }

        public string ImageUrl
        {
            get
            {
                return this.imageUrlProvider.GetImageUrl(this.FileName, WebApplicationConstants.ImageCarWidth);
            }
        }
    }
}