namespace TripDestination.Data.Models
{
    using Common.Infrastructure.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Page
    {
        [Index]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.PageHeadingMinLength, ErrorMessage = "Page heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageHeadingMaxLength, ErrorMessage = "Page heading can no be more than 50 symbols long.")]
        public string Heading { get; set; }

        [Required]
        [MinLength(ModelConstants.PageSubHeadingMinLength, ErrorMessage = "Page subheding heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageSubHeadingMaxLength, ErrorMessage = "Page subheding heading can no be more than 50 symbols long.")]
        public string SubHeading { get; set; }
    }
}
