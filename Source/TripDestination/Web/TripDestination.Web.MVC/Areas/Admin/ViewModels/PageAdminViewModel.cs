namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class PageAdminViewModel : IMapFrom<Page>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.PageHeadingMinLength, ErrorMessage = "Page heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageHeadingMaxLength, ErrorMessage = "Page heading can no be more than 50 symbols long.")]
        public string Heading { get; set; }

        [Required]
        [MinLength(ModelConstants.PageSubHeadingMinLength, ErrorMessage = "Page subheding heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageSubHeadingMaxLength, ErrorMessage = "Page subheding heading can no be more than 50 symbols long.")]
        public string SubHeading { get; set; }

        public int Layout { get; set; }
    }
}