namespace TripDestination.Data.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    public class TripDestinationDbContext : IdentityDbContext<User>, ITripDestinationDbContext
    {
        public TripDestinationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TripDestinationDbContext Create()
        {
            return new TripDestinationDbContext();
        }
    }
}
