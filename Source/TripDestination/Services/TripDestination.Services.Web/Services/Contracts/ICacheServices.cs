namespace TripDestination.Services.Web.Services.Contracts
{
    using System;

    public interface ICacheServices
    {
        T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds);

        void Remove(string itemName);
    }
}
