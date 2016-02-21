namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Constants;
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class TownAdminViewModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.TownNameMinLength, ErrorMessage = "Town name can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.TownNameMaxLength, ErrorMessage = "Town name can no be more than 50 symbols long.")]
        public string Name { get; set; }
    }
}