namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using AutoMapper;
    using Services.Web.Providers.Contracts;
    using Common.Infrastructure.Constants;
    using Services.Web.Providers;
    public class PassengerTripEditInputModel : IMapFrom<PassengerTrip>, IHaveCustomMappings
    {
        private readonly IMediaImageUrlProvider imageUrlProvider;

        public PassengerTripEditInputModel()
        {
            this.imageUrlProvider = new MediaImageUrlProvider();
        }

        public string Username { get; set; }

        public string AvatarFilename { get; set; }

        public string AvatarUrlSmall
        {
            get
            {
                return this.imageUrlProvider.GetImageUrl(this.AvatarFilename, WebApplicationConstants.ImageUserAvatarSmallWidth);
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PassengerTrip, PassengerTripEditInputModel>("Username")
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.UserName));

            configuration.CreateMap<PassengerTrip, PassengerTripEditInputModel>("FirstName")
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName));

            configuration.CreateMap<PassengerTrip, PassengerTripEditInputModel>("LastName")
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName));

            configuration.CreateMap<User, PassengerTripEditInputModel>("AvatarFilename")
                .ForMember(x => x.AvatarFilename, opt => opt.MapFrom(x => x.Avatar.FileName));
        }
    }
}