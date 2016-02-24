namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;
    using System.Web.Mvc;

    [Bind(Exclude = "CreatedOn, FromUserUserName, RatedUserUserName")]
    public class RatingAdminViewModel : IMapFrom<Rating>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FromUserUserName { get; set; }

        public string RatedUserUserName { get; set; }

        public double Value { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<SelectListItem> RateValueSelectList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Value = "1", Text = "1" },
                    new SelectListItem { Value = "2", Text = "2" },
                    new SelectListItem { Value = "3", Text = "3" },
                    new SelectListItem { Value = "4", Text = "4" },
                    new SelectListItem { Value = "5", Text = "5" },
                };
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Rating, RatingAdminViewModel>("FromUserUserName")
                .ForMember(x => x.FromUserUserName, opt => opt.MapFrom(x => x.FromUser.UserName));

            configuration.CreateMap<Rating, RatingAdminViewModel>("RatedUserUserName")
                .ForMember(x => x.RatedUserUserName, opt => opt.MapFrom(x => x.RatedUser.UserName));
        }
    }
}