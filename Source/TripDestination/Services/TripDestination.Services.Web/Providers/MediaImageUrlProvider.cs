namespace TripDestination.Services.Web.Providers
{
    using Common.Infrastructure.Constants;
    using Contracts;

    public class MediaImageUrlProvider : IMediaImageUrlProvider
    {
        public string GetImageUrl(string usernameSlashPhotoFilename, int? size)
        {
            string url = string.Empty;

            if (size != null)
            {
                url = string.Format(
                    "/{0}/{1}/{2}",
                    WebApplicationConstants.ImageRouteUrl,
                    usernameSlashPhotoFilename,
                    size);
            }
            else
            {
                url = string.Format(
                    "/{0}/{1}",
                    WebApplicationConstants.ImageRouteUrl,
                    usernameSlashPhotoFilename);
            }

            return url;
        }
    }
}
