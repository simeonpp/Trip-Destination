namespace TripDestination.Services.Web.Services
{
    using Contracts;
    using ImageProcessor;
    using System.IO;
    using System.Drawing;
    using ImageProcessor.Imaging;
    using ImageProcessor.Imaging.Formats;
    using Common.Infrastructure.Constants;
    using System;
    using System.Web;

    public class ImageProccessorServices : IImageProccessorServices
    {
        public byte[] Resize(byte[] originalImage, int width)
        {
            using (var originalImageStream = new MemoryStream(originalImage))
            {
                using (var resultImage = new MemoryStream())
                {
                    using (var imageFactory = new ImageFactory())
                    {
                        var createdImage = imageFactory
                                .Load(originalImageStream);

                        if (createdImage.Image.Width > width)
                        {
                            createdImage = createdImage
                                .Resize(new ResizeLayer(new Size(width, 0), ResizeMode.Max));
                        }

                        createdImage
                            .Format(new JpegFormat { Quality = WebApplicationConstants.ImageQuality })
                            .Save(resultImage);
                    }

                    return resultImage.GetBuffer();
                }
            }
        }

        public void ResizeAndSaveImage(HttpPostedFileBase originalImage, int width, string originalImageFilePath, string extension)
        {
            byte[] imgData;
            using (var reader = new BinaryReader(originalImage.InputStream))
            {
                imgData = reader.ReadBytes(originalImage.ContentLength);
            }

            var filePath = originalImageFilePath.Substring(0, originalImageFilePath.Length - extension.Length);
            var resizedImageFilePath = filePath + "_" + width + extension;
            byte[] resizedImageBytes = this.Resize(imgData, 210);
            MemoryStream ms = new MemoryStream(resizedImageBytes);
            Image resizedImage = Image.FromStream(ms);
            resizedImage.Save(resizedImageFilePath);
        }
    }
}
