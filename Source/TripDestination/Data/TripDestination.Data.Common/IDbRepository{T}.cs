namespace TripDestination.Data.Common
{
    using System.Linq;
    using Models;

    public interface IDbRepository<T> : IDbRepository<T, int>
    where T : BaseModel<int>
    {
    }

    public interface IDbRepository<T, in TKey> : IDbGenericRepository<T, TKey>
    where T : BaseModel<TKey>
    {
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
