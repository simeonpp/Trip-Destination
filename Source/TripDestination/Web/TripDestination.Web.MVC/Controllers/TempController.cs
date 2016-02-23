namespace TripDestination.Web.MVC.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Drawing;
    using Services.Web.Services.Contracts;
    using Common.Infrastructure.Constants;
    public class TempController : BaseController
    {
        private readonly IImageProccessorServices imageProccessorServices;

        public TempController(IImageProccessorServices imageProccessorServices)
        {
            this.imageProccessorServices = imageProccessorServices;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var USERNAME = "pesho";
                var uploadsFolderPath = this.Server.MapPath("~/App_Data/uploads");
                var folderToBeuploadFiles = Path.Combine(uploadsFolderPath, USERNAME);

                if (!Directory.Exists(folderToBeuploadFiles))
                {
                    Directory.CreateDirectory(folderToBeuploadFiles);
                }

                var originalFileName = Path.GetFileName(file.FileName);
                var sizeInBytes = file.ContentLength;
                var contentType = file.ContentType;
                var extension = Path.GetExtension(originalFileName);
                var fileName = Guid.NewGuid().ToString();
                var fileNameWithExtension = fileName + extension;
                var filePath = Path.Combine(folderToBeuploadFiles, fileNameWithExtension);

                if (sizeInBytes < WebApplicationConstants.ImageMaxSizeInBytes && (extension == ".jpg" || extension == ".png"))
                {
                    file.SaveAs(filePath);
                    //this.imageProccessorServices.ResizeAndSaveImage(file, 210, filePath, extension);
                }
            }

            return this.RedirectToAction("Index");
        }
    }
}