namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;
    using TripDestination.Common.Infrastructure.Constants;
    using Common.Models;
    using System.Collections;
    using System.Collections.Generic;
    public class User : IdentityUser
    {
        private ICollection<Trip> trips;

        private ICollection<View> views;

        public User()
        {
            this.trips = new HashSet<Trip>();
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

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }

        public virtual ICollection<View> Views
        {
            get { return this.views; }
            set { this.views = value; }
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
