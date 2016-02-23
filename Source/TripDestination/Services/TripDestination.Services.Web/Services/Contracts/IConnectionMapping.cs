namespace TripDestination.Services.Web.Services.Contracts
{
    using System.Collections.Generic;

    public interface IConnectionMapping<T>
    {
        int Count();

        void Add(T key, string connectionId);

        IEnumerable<string> GetConnections(T key);

        void Remove(T key, string connectionId);
    }
}
