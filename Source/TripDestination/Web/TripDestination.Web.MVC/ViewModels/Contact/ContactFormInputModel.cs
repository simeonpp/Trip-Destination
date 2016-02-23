namespace TripDestination.Web.MVC.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Constants;
    using TripDestination.Web.Infrastructure.Attributes;

    public class ContactFormInputModel
    {
        [Required]
        [PlaceHolder("First and last name")]
        [MinLength(ViewModelConstants.ContactFormNameMinLength, ErrorMessage = "Name can no be less tha 4 symbols long.")]
        [MaxLength(ViewModelConstants.ContactFormNameMaxLength, ErrorMessage = "Name can no be more than 50 symbols long.")]
        public string Name { get; set; }

        [Required]
        [PlaceHolder("Your email")]
        [RegularExpression(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Invalid E-mail")]
        public string Email { get; set; }

        [Required]
        [PlaceHolder("Enter subject")]
        [MinLength(ViewModelConstants.ContactFormSubjectMinLength, ErrorMessage = "Subject can no be less tha4 symbols long.")]
        [MaxLength(ViewModelConstants.ContactFormSubjectMaxLength, ErrorMessage = "Subject can no be more than 50 symbols long.")]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [UIHint("TextArea")]
        [MinLength(ViewModelConstants.ContactFormMessageMinLength, ErrorMessage = "Message can no be less tha 10 symbols long.")]
        [MaxLength(ViewModelConstants.ContactFormMessageMaxLength, ErrorMessage = "Message can no be more than 500 symbols long.")]
        public string Message { get; set; }
    }
}