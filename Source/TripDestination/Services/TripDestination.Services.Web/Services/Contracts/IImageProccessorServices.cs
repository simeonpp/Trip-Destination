namespace TripDestination.Services.Web.Services.Contracts
{
    using System.Web;

    public interface IImageProccessorServices
    {
        byte[] Resize(byte[] originalImage, int width);

        void ResizeAndSaveImage(HttpPostedFileBase originalImage, int width, string originalImageFilename, string extension);
    }
}
