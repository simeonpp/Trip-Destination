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

        public static string GetProfileUrl(string username)
        {
            return string.Format("/User/{0}", username);
        }

        public static string GetProfileUrl(string username, string firstOrLastName)
        {
            return string.Format("/User/{0}/{1}", username, firstOrLastName);
        }

        public static string GetProfileUrl(string username, string firstname, string lastname)
        {
            return string.Format("/User/{0}/{1}", username, firstname + "-" + lastname);
        }
    }
}
