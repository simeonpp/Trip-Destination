namespace TripDestination.Data.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class TripDestinationDbContext : IdentityDbContext<User>
    {
        public TripDestinationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TripDestinationDbContext Create()
        {
            return new TripDestinationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trip>()
                        .HasRequired(m => m.From)
                        .WithMany(t => t.FromTrips)
                        .HasForeignKey(m => m.FromId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                        .HasRequired(m => m.To)
                        .WithMany(t => t.ToTrips)
                        .HasForeignKey(m => m.ToId)
                        .WillCascadeOnDelete(false);
        }

        public virtual IDbSet<Car> Cars { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Like> Likes { get; set; }

        public virtual IDbSet<Newsletter> Newsletters { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public virtual IDbSet<NotificationType> NotificationTypes { get; set; }

        public virtual IDbSet<PassengerTrip> PassengerTrips { get; set; }

        public virtual IDbSet<Photo> Photos { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public virtual IDbSet<Town> Towns { get; set; }

        public virtual IDbSet<Trip> Trips { get; set; }

        public virtual IDbSet<View> Views { get; set; }

        public virtual IDbSet<Page> Pages { get; set; }

        public virtual IDbSet<PageParagraph> PageParagraphs { get; set; }
    }
}
