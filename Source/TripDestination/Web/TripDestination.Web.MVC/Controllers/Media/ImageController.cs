namespace TripDestination.Web.MVC.Controllers.Media
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using Common.Infrastructure.Constants;
    using Services.Data.Contracts;

    public class ImageController : BaseController
    {
        private readonly IPhotoServices photoService;

        public ImageController(IPhotoServices photoService)
        {
            this.photoService = photoService;
        }

        public ActionResult Index(string username, string guid, int? size)
        {
            var photo = this.photoService.GetByFileName(string.Format(
                "{0}/{1}",
                username,
                guid));

            if (photo == null)
            {
                throw new Exception("Not found image");
            }

            string contentType = photo.ContentType;
            string extension = photo.Extension;
            int length = photo.SizeInBytes;

            string uploadsFolderPath = this.Server.MapPath(WebApplicationConstants.ImageFolder);
            string imagePath = this.GetFilePath(Path.Combine(uploadsFolderPath, photo.FileName), extension, size);
            bool imageExists = System.IO.File.Exists(imagePath);
            if (imageExists)
            {
                return this.File(imagePath, contentType);
            }
            else
            {
                throw new Exception("Not found.");
            }
        }

        private string GetFilePath(string originalFilePath, string extension, int? size)
        {
            string imagePath = string.Empty;

            if (size != null)
            {
                imagePath = string.Format(
                    "{0}_{1}{2}",
                    originalFilePath,
                    size,
                    extension);
            }
            else
            {
                imagePath = originalFilePath + extension;
            }

            return imagePath;
        }
    }
}