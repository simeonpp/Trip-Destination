namespace TripDestination.Services.Web.Providers
{
    using Common.Infrastructure.Constants;
    using Contracts;

    public class MediaImageUrlProvider : IMediaImageUrlProvider
    {
        public string GetImageUrl(string usernameSlashPhotoFilename)
        {
            return string.Format(
                    "{0}{1}{2}",
                    WebApplicationConstants.ImageRouteUrl,
                    "/",
                    usernameSlashPhotoFilename);
        }
    }
}
