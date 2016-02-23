namespace TripDestination.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using TripDestination.Common.Infrastructure.Constants;

    public class View : BaseModel<int>
    {
        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        [Required]
        [MinLength(ModelConstants.IpMinLength, ErrorMessage = "Newsletter IP can no be less tha 11 symbols long.")]
        [MaxLength(ModelConstants.IpMaxLength, ErrorMessage = "Newsletter IP can no be more than 45 symbols long.")]
        public string Ip { get; set; }
    }
}
