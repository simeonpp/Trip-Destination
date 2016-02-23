namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Web.Mvc;
    using AutoMapper;

    [Bind(Exclude = "CreatedOn")]
    public class UserCommentsAdminViewModel : IMapFrom<UserComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<UserComment, UserCommentsAdminViewModel>("Username")
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.UserName));

            configuration.CreateMap<UserComment, UserCommentsAdminViewModel>("Author")
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName));
        }
    }
}