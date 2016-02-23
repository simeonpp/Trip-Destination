namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;

    public class PhotosAdminViewModel : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string ContentType { get; set; }

        public string OriginalName { get; set; }

        public int SizeInBytes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FileName { get; set; }
    }
}