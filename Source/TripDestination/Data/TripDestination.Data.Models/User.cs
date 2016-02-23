namespace TripDestination.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;
    using TripDestination.Common.Infrastructure.Constants;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        private ICollection<Trip> trips;

        private ICollection<UserComment> comments;

        private ICollection<PassengerTrip> tripsAsPassenger;

        public User()
        {
            this.trips = new HashSet<Trip>();
            this.comments = new HashSet<UserComment>();
            this.tripsAsPassenger = new HashSet<PassengerTrip>();
        }

        [Required]
        [MinLength(ModelConstants.UserFirstNameMinLength, ErrorMessage = "User first name can not be less than 3 symbols long.")]
        [MaxLength(ModelConstants.UserFirstNameMaxLength, ErrorMessage = "User first name can not be more than 50 symbols long.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ModelConstants.UserLastNameMinLength, ErrorMessage = "User last name can not be less than 3 symbols long.")]
        [MaxLength(ModelConstants.UserLastNameMaxLength, ErrorMessage = "User last name can not be more than 50 symbols long.")]
        public string LastName { get; set; }

        [MinLength(ModelConstants.UserDescriptionMinLength, ErrorMessage = "Description can not be less than 20 symbols long.")]
        [MaxLength(ModelConstants.UserDescriptionMaxLength, ErrorMessage = "Description can not be more than 1000 symbols long.")]
        public string Description { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public int AvatarId { get; set; }

        public virtual Photo Avatar { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }

        public virtual ICollection<PassengerTrip> TripsAsPassenger
        {
            get { return this.tripsAsPassenger; }
            set { this.tripsAsPassenger = value; }
        }

        public virtual ICollection<UserComment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
