namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using AutoMapper;
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using Infrastructure.HtmlSanitizer;
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        private readonly ISanitizer htmlSanitizer;

        private readonly IMediaImageUrlProvider imageUrlProvider;

        public UserViewModel()
        {
            this.htmlSanitizer = new HtmlSanitizerAdapter();
            this.imageUrlProvider = new MediaImageUrlProvider();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

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

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription
        {
            get
            {
                return this.htmlSanitizer.Sanitize(this.Description);
            }
        }

        //public ICollection<BaseCommentViewModel> Comments { get; set; }

        public CarViewModel Car { get; set; }

        public ICollection<BaseTripViewModel> Trips { get; set; }

        public ICollection<BasePassengerTripViewModel> TripsAsPassenger { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>("AvatarFilename")
                .ForMember(x => x.AvatarFilename, opt => opt.MapFrom(x => x.Avatar.FileName));

            configuration.CreateMap<User, UserViewModel>("Trips")
                .ForMember(x => x.Trips, opt => opt.MapFrom(x => x.Trips.Where(t => t.IsDeleted == false)));

            configuration.CreateMap<User, UserViewModel>("TripsAsPassenger")
                .ForMember(x => x.TripsAsPassenger, opt => opt.MapFrom(x => x.TripsAsPassenger.Where(t => t.Approved == true && t.IsDeleted == false)));
        }
    }
}