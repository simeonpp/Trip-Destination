namespace TripDestination.Services.Web.Providers
{
    using System;
    using System.Text;
    using Common.Infrastructure.Constants;
    using Contracts;

    public class IdentifierProvider : IIdentifierProvider
    {
        public string EncodeId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id + ServicesWebConstants.IdentifierProviderSalt);
            return Convert.ToBase64String(plainTextBytes);
        }

        public int DecodeId(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            var bytesAsString = Encoding.UTF8.GetString(base64EncodedBytes);
            string idAsString = bytesAsString.Replace(ServicesWebConstants.IdentifierProviderSalt, String.Empty);
            int id = int.Parse(idAsString);

            return id;
        }
    }
}
