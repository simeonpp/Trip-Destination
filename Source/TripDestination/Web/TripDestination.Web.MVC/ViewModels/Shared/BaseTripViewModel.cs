namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using AutoMapper;
    using Common.Infrastructure.Mapping;
    using System.Collections.Generic;
    using TripDestination.Data.Models;
    using System;

    public abstract class BaseTripViewModel : BaseTripModel, IMapFrom<Trip>
    {
        public int Id { get; set; }

        public BaseUserViewModel Driver { get; set; }

        public int ViewsCount { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }
    }
}