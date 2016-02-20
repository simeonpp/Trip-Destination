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

        public ActionResult Index(string username, string guid)
        {
            var photo = this.photoService.GetByFileName(string.Format(
                "{0}{1}{2}",
                username,
                '/',
                guid));

            if (photo == null)
            {
                throw new Exception("Not found image");
            }

            string contentType = photo.ContentType;
            string extenstion = photo.Extension;
            int length = photo.SizeInBytes;

            var uploadsFolderPath = this.Server.MapPath(WebApplicationConstants.ImageFolder);
            var imagePath = Path.Combine(uploadsFolderPath, photo.FileName + extenstion);
            return this.File(imagePath, contentType);
        }
    }
}