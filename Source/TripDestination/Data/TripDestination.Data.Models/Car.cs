namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using TripDestination.Common.Infrastructure.Constants;
    using Common.Models;

    public class Car : BaseModel<int>
    {
        private IEnumerable<Photo> photos;

        public Car()
        {
            this.TotalSeats = 5;
            this.photos = new HashSet<Photo>();
            this.SpaceForLugage = SpaceForLugage.None;
        }
        
        [Required]
        [Range(ModelConstants.CarTotalSeatsMinLength, ModelConstants.CarTotalSeatsMaxLength, ErrorMessage = "Car total seat must be between 1 and 5.")]
        public int TotalSeats { get; set; }

        [Required]
        [MinLength(ModelConstants.CarBrandMinLength, ErrorMessage = "Car brand can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarBrandMaxLength, ErrorMessage = "Car brand can no be more than 50 symbols long.")]
        public string Brand { get; set; }

        [Required]
        [MinLength(ModelConstants.CarModelMinLength, ErrorMessage = "Car model can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarModelMaxLength, ErrorMessage = "Car model can no be more than 50 symbols long.")]
        public string Model { get; set; }

        [Required]
        [MinLength(ModelConstants.CarColorMinLength, ErrorMessage = "Car color can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.CarColorMaxLength, ErrorMessage = "Car color can no be more than 50 symbols long.")]
        public string Color { get; set; }

        [Range(ModelConstants.CarYearMinYear, ModelConstants.CarYearMaxYear, ErrorMessage = "Car year is not in range.")]
        public int? Year { get; set; }

        [Required]
        public SpaceForLugage SpaceForLugage { get; set; }

        [MinLength(ModelConstants.CarDescriptionMinLength, ErrorMessage = "Car description can no be less than 20 symbols long.")]
        [MaxLength(ModelConstants.CarDescriptionMaxLength, ErrorMessage = "Car description can no be more than 1000 symbols long.")]
        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public virtual IEnumerable<Photo> Photos
        {
            get { return this.photos; }
            set { this.photos = value; }
        }
    }
}
