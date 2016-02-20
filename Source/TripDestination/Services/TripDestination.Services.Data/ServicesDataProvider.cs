namespace TripDestination.Services.Data
{
    using Common.Infrastructure.Constants;

    internal static class ServicesDataProvider
    {
        public static string GetUserImageSmallUrl(string fileName)
        {
            return string.Format(
                    "/{0}/{1}/{2}",
                    WebApplicationConstants.ImageRouteUrl,
                    fileName,
                    WebApplicationConstants.ImageUserAvatarSmallWidth);
        }
    }
}
