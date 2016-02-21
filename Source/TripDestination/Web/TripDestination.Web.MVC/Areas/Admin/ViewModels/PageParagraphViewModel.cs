namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PageParagraphViewModel : IMapFrom<PageParagraph>
    {
        private readonly IPageParagraphTypeProvider pageParagraphTypeProvider;

        public PageParagraphViewModel()
        {
            this.pageParagraphTypeProvider = new PageParagraphTypeProvider();
        }

        public int Id { get; set; }

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

        public IEnumerable<SelectListItem> TypesSelectList
        {
            get
            {
                return this.pageParagraphTypeProvider.GetPagePargraphTypes();
            }
        }

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
    }
}