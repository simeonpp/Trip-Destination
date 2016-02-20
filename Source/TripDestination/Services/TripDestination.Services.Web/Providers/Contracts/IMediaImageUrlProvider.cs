namespace TripDestination.Services.Web.Providers.Contracts
{
    public interface IMediaImageUrlProvider
    {
        string GetImageUrl(string usernameSlashPhotoFilename);
    }
}
