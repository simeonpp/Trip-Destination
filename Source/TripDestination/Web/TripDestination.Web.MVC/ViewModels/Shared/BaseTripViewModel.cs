namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using AutoMapper;
    using Common.Infrastructure.Mapping;
    using System.Collections.Generic;
    using TripDestination.Data.Models;

    public abstract class BaseTripViewModel : BaseTripModel, IMapFrom<Trip>
    {
        public int Id { get; set; }

        public User Driver { get; set; }
        
        //public IEnumerable<View> Views { get; set; }

        //public IEnumerable<Like> Likes { get; set; }

        //public IEnumerable<Comment> Comments { get; set; }
    }
}