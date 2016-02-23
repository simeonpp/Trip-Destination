namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using System;
    using AutoMapper;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System.Web.Mvc;
    using System.Collections;
    using System.Collections.Generic;
    [Bind(Exclude = "CreatedOn,TripId,UserName")]
    public class LikeAdminViewModel : IMapFrom<Like>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public bool Value { get; set; }

        public int TripId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<SelectListItem> LikeValuesSelectLis
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Value = "true", Text = "Yes" },
                    new SelectListItem { Value = "false", Text = "No" }
                };
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Like, LikeAdminViewModel>("UserName")
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName));
        }
    }
}