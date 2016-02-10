namespace TripDestination.Data.Models
{
    using System;
    using Common.Infrastructure.Constants;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ContactForm
    {
        public ContactForm()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Index]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.ContactFormNameMinLength, ErrorMessage = "Name can no be less tha 4 symbols long.")]
        [MaxLength(ModelConstants.ContactFormNameMaxLength, ErrorMessage = "Name can no be more than 50 symbols long.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage = "Invalid E-mail")]
        public string Email { get; set; }

        [Required]
        [MinLength(ModelConstants.ContactFormSubjectMinLength, ErrorMessage = "Subject can no be less than 4 symbols long.")]
        [MaxLength(ModelConstants.ContactFormSubjectMaxLength, ErrorMessage = "Subject can no be more than 50 symbols long.")]
        public string Subject { get; set; }

        [Required]
        [MinLength(ModelConstants.ContactFormMessageMinLength, ErrorMessage = "Message can no be less tha 10 symbols long.")]
        [MaxLength(ModelConstants.ContactFormMessageMaxLength, ErrorMessage = "Message can no be more than 500 symbols long.")]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MinLength(ModelConstants.ContactFormIpMinLength, ErrorMessage = "Contact form ip can no be less than 11 symbols long.")]
        [MaxLength(ModelConstants.ContactFormIpMaxLength, ErrorMessage = "Contact form ip can no be more than 45 symbols long.")]
        public string Ip { get; set; }
    }
}
