namespace TripDestination.Web.MVC.ViewModels.AJAX
{
    using System.ComponentModel.DataAnnotations;
    using Common.Infrastructure.Constants;

    public class NewsletterInputModel
    {
        [Required]
        [RegularExpression(ModelConstants.EmailRegEx, ErrorMessage = "Invalid E-mail")]
        public string Email { get; set; }
    }
}