using System.ComponentModel.DataAnnotations;

namespace TripDestination.Web.MVC.ViewModels.Contact
{
    public class ContactFormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}