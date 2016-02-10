﻿namespace TripDestination.Data.Models
{
    using System.Collections.Generic;
    using Common.Infrastructure.Constants;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Page
    {
        private IEnumerable<PageParagraph> paragraphs;

        public Page()
        {
            this.paragraphs = new HashSet<PageParagraph>();
        }

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

        public string Slug { get; set; }

        public virtual IEnumerable<PageParagraph> Paragraphs
        {
            get { return this.paragraphs; }
            set { this.paragraphs = value; }
        }
    }
}
