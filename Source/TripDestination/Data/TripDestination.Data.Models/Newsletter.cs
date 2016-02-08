﻿namespace TripDestination.Data.Models
{
    using System;
    using Common.Infrastructure.Constants;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Newsletter
    {
        public Newsletter()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Index]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.NewsletterEmailMinLength, ErrorMessage = "Newsletter email can no be less tha 6 symbols long.")]
        [MaxLength(ModelConstants.NewsletterEmailMaxLength, ErrorMessage = "Newsletter email can no be more than 100 symbols long.")]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MinLength(ModelConstants.NewsletterIpMinLength, ErrorMessage = "Newsletter IP can no be less tha 11 symbols long.")]
        [MaxLength(ModelConstants.NewsletterIpMaxLength, ErrorMessage = "Newsletter IP can no be more than 45 symbols long.")]
        public string Ip { get; set; }

        [Required]
        [MinLength(ModelConstants.NewsletterUserAgenMinLength, ErrorMessage = "Newsletter email can no be less tha 200 symbols long.")]
        [MaxLength(ModelConstants.NewsletterUserAgentMaxLength, ErrorMessage = "Newsletter email can no be more than 2000 symbols long.")]
        public string UserAgent { get; set; }
    }
}