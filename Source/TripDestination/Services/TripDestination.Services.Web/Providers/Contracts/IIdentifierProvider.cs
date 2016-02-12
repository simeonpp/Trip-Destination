namespace TripDestination.Services.Web.Providers.Contracts
{
    public interface IIdentifierProvider
    {
        string EncodeId(int id);

        int DecodeId(string urlId);
    }
}
