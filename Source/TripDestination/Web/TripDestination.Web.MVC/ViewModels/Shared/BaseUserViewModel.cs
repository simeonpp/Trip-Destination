namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using AutoMapper;
    using Services.Web.Providers.Contracts;
    using Services.Web.Providers;

    public class BaseUserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        private readonly IMediaImageUrlProvider imageUrlProvider;

        public BaseUserViewModel()
        {
            this.imageUrlProvider = new MediaImageUrlProvider();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string AvatarFilename { get; set; }

        public string AvatarUrl
        {
            get
            {
                return this.imageUrlProvider.GetImageUrl(this.AvatarFilename);
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, BaseUserViewModel>("AvatarFilename")
                .ForMember(x => x.AvatarFilename, opt => opt.MapFrom(x => x.Avatar.FileName));
        }
    }
}