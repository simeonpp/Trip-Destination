namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;

    public class BaseCommentViewModel : IMapFrom<BaseComment>
    {
        public BaseUserViewModel Author { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FormattedCreatedOn
        {
            get
            {
                return this.CreatedOn.ToString("dd.MM.yyyy HH:mm");
            }
        }
    }
}