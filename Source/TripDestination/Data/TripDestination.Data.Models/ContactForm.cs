namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using TripDestination.Common.Infrastructure.Constants;

    public class ContactForm : BaseModel<int>
    {
        [Required]
        [MinLength(ModelConstants.ContactFormNameMinLength, ErrorMessage = "Name can no be less tha 4 symbols long.")]
        [MaxLength(ModelConstants.ContactFormNameMaxLength, ErrorMessage = "Name can no be more than 50 symbols long.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
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
        [MinLength(ModelConstants.IpMinLength, ErrorMessage = "Contact form ip can no be less than 11 symbols long.")]
        [MaxLength(ModelConstants.IpMaxLength, ErrorMessage = "Contact form ip can no be more than 45 symbols long.")]
        public string Ip { get; set; }
    }
}
