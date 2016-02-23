namespace TripDestination.Data.Data
{
    using System;
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface ITripDestinationDbContext : IDisposable
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Car> Cars { get; set; }

        IDbSet<TripComment> TripComments { get; set; }

        IDbSet<UserComment> UserComments { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Newsletter> Newsletters { get; set; }

        IDbSet<Notification> Notifications { get; set; }

        IDbSet<NotificationType> NotificationTypes { get; set; }

        IDbSet<PassengerTrip> PassengerTrips { get; set; }

        IDbSet<Photo> Photos { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        IDbSet<Town> Towns { get; set; }

        IDbSet<Trip> Trips { get; set; }

        IDbSet<View> Views { get; set; }

        IDbSet<Page> Pages { get; set; }

        IDbSet<PageParagraph> PageParagraphs { get; set; }

        IDbSet<ContactForm> ContactForms { get; set; }

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;
    }
}
