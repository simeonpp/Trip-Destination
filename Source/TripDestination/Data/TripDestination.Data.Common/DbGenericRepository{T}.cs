namespace TripDestination.Data.Common
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DbGenericRepository<T, TKey> : IDbGenericRepository<T, TKey>
        where T : class
    {
        public DbGenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; }

        private DbContext Context { get; }

        public IQueryable<T> All()
        {
            return this.DbSet;
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public void Reload(T entity)
        {
            this.Context.Entry<T>(entity).Reload();
        }
    }
}
