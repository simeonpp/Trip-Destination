namespace TripDestination.Tests.Services.Web
{
    using NUnit.Framework;
    using TripDestination.Services.Web.Providers.Contracts;
    using TripDestination.Services.Web.Providers;
    using Common.Infrastructure.Constants;

    [TestFixture]
    public class MediaImageUrlProviderTests
    {
        [Test]
        public void GetImageUrlShouldReturnCorrectUrlWithNoSizeGiven()
        {
            const string fileName = "pesho";
            IMediaImageUrlProvider mediaImageUrlProvider = new MediaImageUrlProvider();
            var actual = mediaImageUrlProvider.GetImageUrl(fileName, null);
            Assert.AreEqual("/" + WebApplicationConstants.ImageRouteUrl + "/pesho", actual);
        }

        [Test]
        public void GetImageUrlShouldReturnCorrectUrlWithSize()
        {
            const string fileName = "pesho";
            const int size = 250;

            IMediaImageUrlProvider mediaImageUrlProvider = new MediaImageUrlProvider();
            var actual = mediaImageUrlProvider.GetImageUrl(fileName, size);
            Assert.AreEqual("/" + WebApplicationConstants.ImageRouteUrl + "/pesho/" + size, actual);
        }
    }
}
