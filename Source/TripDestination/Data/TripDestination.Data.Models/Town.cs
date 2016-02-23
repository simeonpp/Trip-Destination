namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Constants;

    public class Town : BaseModel<int>
    {
        private ICollection<Trip> fromTrips;

        private ICollection<Trip> toTrips;

        public Town()
        {
            this.fromTrips = new HashSet<Trip>();
            this.toTrips = new HashSet<Trip>();
        }

        [Required]
        [MinLength(ModelConstants.TownNameMinLength, ErrorMessage = "Town name can no be less than 3 symbols long.")]
        [MaxLength(ModelConstants.TownNameMaxLength, ErrorMessage = "Town name can no be more than 50 symbols long.")]
        public string Name { get; set; }

        public virtual ICollection<Trip> FromTrips
        {
            get { return this.fromTrips; }
            set { this.fromTrips = value; }
        }

        public virtual ICollection<Trip> ToTrips
        {
            get { return this.toTrips; }
            set { this.toTrips = value; }
        }
    }
}
