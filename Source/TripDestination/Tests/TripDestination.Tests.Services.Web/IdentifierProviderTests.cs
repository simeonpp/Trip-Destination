namespace TripDestination.Tests.Services.Web
{
    using NUnit.Framework;
    using TripDestination.Services.Web.Providers;
    using TripDestination.Services.Web.Providers.Contracts;

    [TestFixture]
    public class IdentifierProviderTests
    {
        [Test]
        public void EncodingAndDecodingShouldWorkCorrectly()
        {
            const int id = 56;

            IIdentifierProvider identProvider = new IdentifierProvider();
            var endodedId = identProvider.EncodeId(id);
            var actual = identProvider.DecodeId(endodedId);
            Assert.AreEqual(id, actual);
        }
    }
}
