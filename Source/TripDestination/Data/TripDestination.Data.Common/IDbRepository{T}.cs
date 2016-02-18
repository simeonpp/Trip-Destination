namespace TripDestination.Data.Common
{
    using System.Linq;
    using Models;
    using System.Data.Entity;

    public interface IDbRepository<T> : IDbRepository<T, int>
    where T : BaseModel<int>
    {
    }

    public interface IDbRepository<T, in TKey> : IDbGenericRepository<T, TKey>
    where T : BaseModel<TKey>
    {
        DbContext Context { get; }

        T GetById(TKey id);

        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);
    }

    public interface IDbGenericRepository<T, in TKey>
    where T : class
    {
        IQueryable<T> All();

        void Add(T entity);

        void Delete(T entity);

        void Reload(T entity);

        void Save();
    }
}
