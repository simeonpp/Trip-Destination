namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TripDestination.Common.Infrastructure.Constants;

    public class Page : BaseModel<int>
    {
        private IEnumerable<PageParagraph> paragraphs;

        public Page()
        {
            this.Layout = 0;
            this.paragraphs = new HashSet<PageParagraph>();
        }

        [Required]
        [MinLength(ModelConstants.PageHeadingMinLength, ErrorMessage = "Page heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageHeadingMaxLength, ErrorMessage = "Page heading can no be more than 50 symbols long.")]
        public string Heading { get; set; }

        [Required]
        [MinLength(ModelConstants.PageSubHeadingMinLength, ErrorMessage = "Page subheding heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageSubHeadingMaxLength, ErrorMessage = "Page subheding heading can no be more than 50 symbols long.")]
        public string SubHeading { get; set; }

        public string Slug { get; set; }

        public int Layout { get; set; }

        public virtual IEnumerable<PageParagraph> Paragraphs
        {
            get { return this.paragraphs; }
            set { this.paragraphs = value; }
        }
    }
}
