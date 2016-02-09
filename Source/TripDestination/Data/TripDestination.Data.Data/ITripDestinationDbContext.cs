namespace TripDestination.Data.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface ITripDestinationDbContext : IDisposable
    {
        int SaveChanges();

        //IDbSet<User> Users { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
