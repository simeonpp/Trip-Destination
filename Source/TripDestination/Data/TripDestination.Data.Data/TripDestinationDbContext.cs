namespace TripDestination.Data.Data
{
    using System;
    using Common.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;
    using System.Linq;

    public class TripDestinationDbContext : IdentityDbContext<User>
    {
        public TripDestinationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Car> Cars { get; set; }

        public virtual IDbSet<TripComment> TripComments { get; set; }

        public virtual IDbSet<UserComment> UserComments { get; set; }

        public virtual IDbSet<Like> Likes { get; set; }

        public virtual IDbSet<Newsletter> Newsletters { get; set; }

        public virtual IDbSet<TripNotification> TripNotifications { get; set; }

        public virtual IDbSet<NotificationType> NotificationTypes { get; set; }

        public virtual IDbSet<PassengerTrip> PassengerTrips { get; set; }

        public virtual IDbSet<Photo> Photos { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public virtual IDbSet<Town> Towns { get; set; }

        public virtual IDbSet<Trip> Trips { get; set; }

        public virtual IDbSet<View> Views { get; set; }

        public virtual IDbSet<Page> Pages { get; set; }

        public virtual IDbSet<PageParagraph> PageParagraphs { get; set; }

        public virtual IDbSet<ContactForm> ContactForms { get; set; }

        public static TripDestinationDbContext Create()
        {
            return new TripDestinationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
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

            // http://stackoverflow.com/questions/6531671/what-does-principal-end-of-an-association-means-in-11-relationship-in-entity-fr
            modelBuilder.Entity<Car>()
                .HasRequired(c => c.Owner)
                .WithOptional(u => u.Car);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
