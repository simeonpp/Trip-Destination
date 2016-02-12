namespace TripDestination.Web.MVC.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Constants;

    public class ContactFormViewModel
    {
        [Required]
        [MinLength(ViewModelConstants.ContactFormNameMinLength, ErrorMessage = "Name can no be less tha 4 symbols long.")]
        [MaxLength(ViewModelConstants.ContactFormNameMaxLength, ErrorMessage = "Name can no be more than 50 symbols long.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
            ErrorMessage = "Invalid E-mail")]
        public string Email { get; set; }

        [Required]
        [MinLength(ViewModelConstants.ContactFormSubjectMinLength, ErrorMessage = "Subject can no be less tha4 symbols long.")]
        [MaxLength(ViewModelConstants.ContactFormSubjectMaxLength, ErrorMessage = "Subject can no be more than 50 symbols long.")]
        public string Subject { get; set; }

        [Required]
        [MinLength(ViewModelConstants.ContactFormMessageMinLength, ErrorMessage = "Message can no be less tha 10 symbols long.")]
        [MaxLength(ViewModelConstants.ContactFormMessageMaxLength, ErrorMessage = "Message can no be more than 500 symbols long.")]
        public string Message { get; set; }
    }
}