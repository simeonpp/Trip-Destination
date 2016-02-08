namespace TripDestination.Data.Models
{
    using Common.Infrastructure.Constants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Town
    {
        private ICollection<Trip> fromTrips;

        private ICollection<Trip> toTrips;

        public Town()
        {
            this.fromTrips = new HashSet<Trip>();
            this.toTrips = new HashSet<Trip>();
        }

        [Index]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.TownNameMinLength, ErrorMessage = "Town name can no be less than 4 symbols long.")]
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
