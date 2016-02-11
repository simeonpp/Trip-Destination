namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using AutoMapper;
    using Common.Infrastructure.Mapping;
    using System.Collections.Generic;
    using TripDestination.Data.Models;

    public abstract class BaseTripViewModel : BaseTripModel, IMapFrom<Trip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string DriverFirstName { get; set; }

        public string DriverLastName { get; set; }

        public string DriverImageSrc { get; set; }
        
        //public IEnumerable<View> Views { get; set; }

        //public IEnumerable<Like> Likes { get; set; }

        //public IEnumerable<Comment> Comments { get; set; }

        public virtual void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Trip, BaseTripViewModel>("DriverFirstName")
                .ForMember(m => m.DriverFirstName, opt => opt.MapFrom(t => t.Driver.FirstName));
            configuration.CreateMap<Data.Models.Trip, BaseTripViewModel>("DriverLastName")
                .ForMember(m => m.DriverLastName, opt => opt.MapFrom(t => t.Driver.LastName));

            // TODO: Implement avatar photos
            //configuration.CreateMap<Data.Models.Trip, BaseTripModel>("DriverImageSrc")
            //    .ForMember(m => m.DriverImageSrc, opt => opt.MapFrom(t => t.Driver.Id));
        }
    }
}