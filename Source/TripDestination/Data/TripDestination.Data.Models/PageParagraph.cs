namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using TripDestination.Common.Infrastructure.Constants;

    public class PageParagraph : BaseModel<int>
    {
        public PageParagraph()
        {
            this.Type = PageParagraphType.Normal;
        }

        [Required]
        [MinLength(ModelConstants.PageParagraphMainHeadingMinLength, ErrorMessage = "Page paragraph main heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageParagraphMainHeadingMaxLength, ErrorMessage = "Page paragraph main heading can no be more than 50 symbols long.")]
        public string MainHeading { get; set; }

        [Required]
        [MinLength(ModelConstants.PageParagraphMainSubHeadingMinLength, ErrorMessage = "Page paragraph main subheading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageParagraphMainSubHeadingMaxLength, ErrorMessage = "Page paragraph main subheading can no be more than 50 symbols long.")]
        public string MainSubHeading { get; set; }

        [Required]
        public PageParagraphType Type { get; set; }

        [Required]
        [MinLength(ModelConstants.PageParagraphHeadingMinLength, ErrorMessage = "Page paragraph heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageParagraphHeadingMaxLength, ErrorMessage = "Page paragraph heading can no be more than 50 symbols long.")]
        public string Heading { get; set; }

        [Required]
        [MinLength(ModelConstants.PageParagraphTextMinLength, ErrorMessage = "Page paragraph text can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageParagraphTextMaxLength, ErrorMessage = "Page paragraph text can no be more than 50 symbols long.")]
        public string Text { get; set; }

        [MinLength(ModelConstants.PageParagraphHeadingMinLength, ErrorMessage = "Page paragraph additional heading can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageParagraphHeadingMaxLength, ErrorMessage = "Page paragraph additional heading can no be more than 50 symbols long.")]
        public string AdditionalHeading { get; set; }

        [MinLength(ModelConstants.PageParagraphTextMinLength, ErrorMessage = "Page paragraph additional text can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.PageParagraphTextMaxLength, ErrorMessage = "Page paragraph additional text can no be more than 50 symbols long.")]
        public string AdditionalText { get; set; }

        [Required]
        public int PageId { get; set; }

        public virtual Page Page { get; set; }
    }
}
