namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using Infrastructure.HtmlSanitizer;
    using System.Collections.Generic;

    public class ExtendedUserViewModel : BaseUserViewModel, IMapFrom<User>
    {
        private readonly ISanitizer htmlSanitizer;

        public ExtendedUserViewModel()
        {
            this.htmlSanitizer = new HtmlSanitizerAdapter();
        }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription
        {
            get
            {
                return this.htmlSanitizer.Sanitize(this.Description);
            }
        }

        public ICollection<UserCommentViewModel> Comments { get; set; }

        public CarViewModel Car { get; set; }
    }
}