namespace TripDestination.Data.Models
{
    using Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TripDestination.Common.Infrastructure.Constants;
    public class Photo
    {
        public Photo()
        {
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.PhotoContentTypeMinLength, ErrorMessage = "Photo content type should be more than 5 symbols long.")]
        [MaxLength(ModelConstants.PhotoContentTypeMaxLength, ErrorMessage = "Photo content type should be less than 75 symbols long.")]
        public string ContentType { get; set; }

        [Required]
        [MinLength(ModelConstants.PhotoExtensionMinLength, ErrorMessage = "Photo extension should be more than 3 symbols long.")]
        [MaxLength(ModelConstants.PhotoExtensionMaxLength, ErrorMessage = "Photo extension should be less than 15 symbols long.")]
        public string Extension { get; set; }

        [Required]
        [MinLength(ModelConstants.PhotoOriginalNameMinLength, ErrorMessage = "Photo original name should be more than 4 symbols long.")]
        [MaxLength(ModelConstants.PhotoOriginalNameMaxLength, ErrorMessage = "Photo original should be less than 255 symbols long.")]
        public string OriginalName { get; set; }

        [Required]
        public int SizeInKB { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MinLength(ModelConstants.PhotoFileNameMinLength, ErrorMessage = "Photo file name should be more than 25 symbols long.")]
        [MaxLength(ModelConstants.PhotoFileNameMaxLength, ErrorMessage = "Photo file should be less than 70 symbols long.")]
        public string FileName { get; set; }
    }
}