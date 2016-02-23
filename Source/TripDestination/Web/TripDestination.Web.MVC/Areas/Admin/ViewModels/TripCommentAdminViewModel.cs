namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using AutoMapper;

    public class TripCommentAdminViewModel : IMapFrom<TripComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int TripId { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<UserComment, UserCommentsAdminViewModel>("Author")
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName));
        }
    }
}