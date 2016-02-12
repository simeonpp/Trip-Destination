namespace TripDestination.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;
    using TripDestination.Common.Infrastructure.Constants;

    public class ContactForm : BaseModel<int>
    {
        [Required]
        [MinLength(ModelConstants.ContactFormNameMinLength, ErrorMessage = "Name can no be less tha 4 symbols long.")]
        [MaxLength(ModelConstants.ContactFormNameMaxLength, ErrorMessage = "Name can no be more than 50 symbols long.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelConstants.ContactFormEmailMaxLength, ErrorMessage = "Message can no be more than 100 symbols long.")]
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
        [MinLength(ModelConstants.ContactFormIpMinLength, ErrorMessage = "Contact form ip can no be less than 11 symbols long.")]
        [MaxLength(ModelConstants.ContactFormIpMaxLength, ErrorMessage = "Contact form ip can no be more than 45 symbols long.")]
        public string Ip { get; set; }
    }
}
